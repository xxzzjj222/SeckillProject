using Newtonsoft.Json;
using Projects.Cores.Exceptions;
using Projects.Cores.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

namespace Projects.Cores.Middleware
{
    public class HttpMiddleService : IMiddleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string HttpConst = "micro";

        public HttpMiddleService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private MiddleResult GetMiddleResult(HttpResponseMessage httpResponseMessage)
        {
            //将httpResponseMessage转换为MiddleResult
            HttpStatusCode status = httpResponseMessage.StatusCode;
            if (status.Equals(HttpStatusCode.OK)|| status.Equals(HttpStatusCode.Created)|| status.Equals(HttpStatusCode.Accepted))
            {
                string httpJsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return MiddleResult.JsonToMiddleResult(httpJsonString);
            }
            else
            {
                throw new FrameException($"{HttpConst}服务调用错误:{httpResponseMessage.Content.ReadAsStringAsync()}");
            }
        }
        public MiddleResult Delete(string middleUrl, IDictionary<string, object> middleParam)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.Delete请求
            HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync(middleUrl).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Get(string middleUrl, IDictionary<string, object> middleParam)
        {
            //1.获取httpClient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.参数转换为url方式
            string urlParam = HttpParamUtil.DicToHttpUrlParam(middleParam);

            //3.Get请求
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(middleUrl + urlParam).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string middleUrl, IDictionary<string, object> middleParam)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            //3.Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl,httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult PostDynamic(string middleUrl, dynamic middleParam)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            //3.Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            //3.Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string middleUrl, IDictionary<string, object> middleParam)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            //3.Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            //3.Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult PutDynamic(string middleUrl, dynamic middleParam)
        {
            //1.获取httpclient
            HttpClient httpClient = _httpClientFactory.CreateClient(HttpConst);

            //2.转换成json格式
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            //3.Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }
    }
}
