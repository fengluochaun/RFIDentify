using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
	public class HttpHelper
	{
		private static readonly object LockObj = new object();
		private HttpClient? client = null;
		private string BASE_ADDRESS { get; set; } = "http://localhost:7195/";
		public HttpHelper()
		{
			GetInstance();
		}
		public HttpHelper(string baseAddress)
		{
			BASE_ADDRESS = baseAddress;
			GetInstance();
		}
		/// <summary>
		/// 制造单例
		/// </summary>
		/// <returns></returns>
		public HttpClient GetInstance()
		{
			if (client == null)
			{
				lock (LockObj)
				{
					if (client == null)
					{
						client = new HttpClient()
						{
							BaseAddress = new Uri(BASE_ADDRESS)
						};
					}
				}
			}
			return client;
		}

		/// <summary>
		/// 异步Post请求
		/// </summary>
		/// <param name="url">请求地址</param>
		/// <param name="strJson">传入的数据</param>
		/// <returns></returns>
		public async Task<string> PostAsync(string url, string strJson)
		{
			try
			{
				HttpContent content = new StringContent(strJson);
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
				HttpResponseMessage res = await client!.PostAsync(url, content);
				if (res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.Created)
				{
					string resMsgStr = await res.Content.ReadAsStringAsync();
					return resMsgStr;
				}
				else
				{
					return null!;
				}
			}
			catch (Exception)
			{
				return null!;
			}
		}

		/// <summary>
		/// 异步Post请求
		/// </summary>
		/// <typeparam name="TResult">返回参数的数据类型</typeparam>
		/// <param name="url">请求地址</param>
		/// <param name="data">传入的数据</param>
		/// <returns></returns>
		public async Task<TResult> PostAsync<TResult>(string url, object data)
		{
			try
			{
				HttpContent content;
				if (data is Dictionary<string, string> dics)
				{
					content = new FormUrlEncodedContent(dics);
				}
				else
				{
					var jsonData = JsonConvert.SerializeObject(data);
					content = new StringContent(jsonData);
				}
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
				HttpResponseMessage res = await client!.PostAsync(url, content);
				if (res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.Created)
				{
					string resMsgStr = await res.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<TResult>(resMsgStr);
					return result != null ? result! : default!;
				}
				else
				{
					MessageBox.Show(res.StatusCode.ToString());
					return default!;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return default!;
			}
		}
		/// <summary>
		/// 异步Get请求
		/// </summary>
		/// <typeparam name="TResult">返回参数的数据</typeparam>
		/// <param name="url">请求地址</param>
		/// <returns></returns>
		public async Task<TResult> GetAsync<TResult>(string url)
		{
			try
			{
				var resMsgStr = await client!.GetStringAsync(url);
				var result = JsonConvert.DeserializeObject<ResultDto<TResult>>(resMsgStr);
				return result != null ? result.Data! : default!;
			}
			catch (Exception ex)
			{
				return default!;
			}
		}

		public async Task<TResult> DeleteAsync<TResult>(string url)
		{
			try
			{
				var resMsgStr = await client!.DeleteAsync(url);
				var result = JsonConvert.DeserializeObject<ResultDto<TResult>>(resMsgStr.ToString());
				return result != null ? result.Data! : default!;
			}
			catch (Exception ex)
			{
				return default!;
			}
		}

		public async Task<TResult> PutAsync<TResult>(string url, object data)
		{
			try
			{
				var jsonData = JsonConvert.SerializeObject(data);
				HttpContent content = new StringContent(jsonData);
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
				HttpResponseMessage res = await client!.PutAsync(url, content);
				if (res.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string resMsgStr = await res.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ResultDto<TResult>>(resMsgStr);
					return result != null ? result.Data! : default!;
				}
				else
				{
					MessageBox.Show(res.StatusCode.ToString());
					return default!;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return default!;
			}
		}
	}
	public class ResultDto<TResult>
	{
		public string? Msg { get; set; }
		public TResult? Data { get; set; }
		public bool? Success { get; set; }
	}
}
