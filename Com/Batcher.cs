using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace RFIDentify.Com
{
    public sealed class Batcher<T> where T : class
    {
        private readonly int _interval;
        private readonly int _batchSize;
        private readonly Action<Batch<T>> _processor;
        private volatile Container _container;
        private readonly Timer _timer;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public Batcher(
            Action<Batch<T>> processor,
            int batchSize,
            TimeSpan interval)
        {
            _interval = (int)interval.TotalMilliseconds;
            _batchSize = batchSize;
            _processor = processor;
            _container = new Container(batchSize);
            _timer = new Timer(_ => Process(), null, _interval, Timeout.Infinite);
        }

        private void Process()
        {
            if (_container.IsEmpty) return;
            _lock.EnterWriteLock();
            try
            {
                if (_container.IsEmpty) return;
                var container = Interlocked.Exchange(
                    ref _container, new Container(_batchSize));
                _ = Task.Run(() => _processor(container.AsBatch()));
                _timer.Change(_interval, Timeout.Infinite);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Add(T item)
        {
            _lock.EnterReadLock();
            bool success = false;
            try
            {
                success = _container.TryAdd(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }

            if (!success)
            {
                Process();
                new SpinWait().SpinOnce();
                Add(item);
            }
        }

        private sealed class Container
        {
            private volatile int _next = -1;
            private readonly T[] _data;
            public bool IsEmpty
                => _next == -1;
            public Container(int batchSize)
                => _data = Batch<T>.CreatePooledArray(batchSize);
            public bool TryAdd(T item)
            {
                var index = Interlocked.Increment(ref _next);
                if (index > _data.Length - 1) return false;
                _data[index] = item;
                return true;
            }
            public Batch<T> AsBatch() => new Batch<T>(_data);
        }
    }

}
