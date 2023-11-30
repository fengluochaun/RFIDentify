using javax.xml.transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RFIDentify.Com
{
    public class WebUtils
    {
        public static async Task<ApiResponse> InvokeWebapi(string url, string api, string type, Dictionary<string, string> dics)
        {
            try
            {
                var result = new ApiResponse();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("authorization", "Basic YWRtaW46cGFzc3dvcmRAcmljZW50LmNvbQ==");//basic编码后授权码
                client.BaseAddress = new Uri(url);

                client.Timeout = TimeSpan.FromSeconds(510);

                if (type.ToLower() == "put")
                {
                    HttpResponseMessage response;
                    //包含复杂类型
                    if (dics.Keys.Contains("input"))
                    {
                        if (dics != null)
                        {
                            foreach (var item in dics.Keys)
                            {
                                api = api.Replace(item, dics[item]).Replace("{", "").Replace("}", "");
                            }
                        }
                        var contents = new StringContent(dics["input"], Encoding.UTF8, "application/json");
                        response = client.PutAsync(api, contents).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                            return result;
                        }
                        return result;
                    }

                    var content = new FormUrlEncodedContent(dics);
                    response = client.PutAsync(api, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                        return result;
                    }
                }
                else if (type.ToLower() == "post")
                {
                    var content = new FormUrlEncodedContent(dics);

                    HttpResponseMessage response = client.PostAsync(api, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                        return result;
                    }
                }
                else if (type.ToLower() == "get")
                {
                    HttpResponseMessage response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                        return result;
                    }
                }
                else
                {
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ApiResponse() {
                    Status = "100",
                    Message = ex.Message,
                };
            }           

        }
    }

    // 用于反序列化 API 响应的类
    public class ApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int Result { get; set; }
    }
}
