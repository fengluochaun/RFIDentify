using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
    public sealed class Batch<T> : IEnumerable<T>, IDisposable where T : class
    {
        private bool _isDisposed;
        private int? _count;
        private readonly T[] _data;
        private static readonly ArrayPool<T> _pool
            = ArrayPool<T>.Create();

        public int Count
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(Batch<T>));
                if (_count.HasValue)
                    return _count.Value;
                var count = 0;
                for (int index = 0; index < _data.Length; index++)
                {
                    if (_data[index] is null)
                    {
                        break;
                    }
                    count++;
                }
                return (_count = count).Value;
            }
        }
        public Batch(T[] data)
           => _data = data
               ?? throw new ArgumentNullException(nameof(data));
        public void Dispose()
        {
            _pool.Return(_data, clearArray: true);
            _isDisposed = true;
        }
        public IEnumerator<T> GetEnumerator()
           => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator()
           => GetEnumerator();
        public static T[] CreatePooledArray(int batchSize)
           => _pool.Rent(batchSize);
        private void EnsureNotDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(Batch<T>));
        }

        private sealed class Enumerator : IEnumerator<T>
        {
            private readonly Batch<T> _batch;
            private readonly T[] _data;
            private int _index = -1;
            public Enumerator(Batch<T> batch)
            {
                _batch = batch;
                _data = batch._data;
            }
            public T Current
            {
                get
                {
                    _batch.EnsureNotDisposed();
                    return _data[_index];
                }
            }
            object IEnumerator.Current => Current;
            public void Dispose() { }
            public bool MoveNext()
            {
                _batch.EnsureNotDisposed();
                return ++_index < _data.Length && _data[_index] != null;
            }
            public void Reset()
            {
                _batch.EnsureNotDisposed();
                _index = -1;
            }
        }
    }

}
