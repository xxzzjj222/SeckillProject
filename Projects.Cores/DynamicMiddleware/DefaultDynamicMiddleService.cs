using Projects.Cores.DynamicMiddleware.Urls;
using Projects.Cores.Exceptions;
using Projects.Cores.Middleware;
using Projects.Cores.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware
{
    public class DefaultDynamicMiddleService : IDynamicMiddlewareService
    {
        private IMiddleService _middleService;//中台组件
        private IDynamicMiddleUrl _dynamicMiddleUrl;//动态url组件

        public DefaultDynamicMiddleService(IMiddleService middleService,IDynamicMiddleUrl dynamicMiddleUrl)
        {
            _middleService = middleService;
            _dynamicMiddleUrl = dynamicMiddleUrl;
        }
        public void Delete(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            //1.获取中台url https://localhost:5001   servicenode localhost:5001
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            //2.请求
            MiddleResult middleResult=_middleService.Delete(url + serviceLink, middleParam);

            //3.判断是否成功
            IsSuccess(middleResult);
        }

        public dynamic DeleteDynamic(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Delete(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public IDictionary<string, object> Get(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.resultDic;
        }

        public T Get<T>(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam) where T : new()
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            // 4、结果进行转换对象
            return ConvertUtil.MiddleResultToObject<T>(middleResult);
        }

        public dynamic GetDynamic(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
           string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public IList<IDictionary<string, object>> GetList(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.resultList;
        }

        public IList<T> GetList<T>(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam) where T : new()
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            // 4、结果进行转换对象
            return ConvertUtil.MiddleResultToList<T>(middleResult);
        }

        public void Post(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Post(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public void Post(string urlScheme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Post(url + serviceLink, middleParams);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public dynamic PostDynamic(string urlScheme, string serviceName, string serviceLink, dynamic middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.PostDynamic(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public void Put(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Put(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public void Put(string urlScheme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Put(url + serviceLink, middleParams);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public dynamic PutDynamic(string urlScheme, string serviceName, string serviceLink, dynamic middleParam)
        {
            // 1、获取中台url
            string url = _dynamicMiddleUrl.GetMiddleUrl(urlScheme, serviceName);

            // 2、请求
            MiddleResult middleResult = _middleService.Put(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        private void IsSuccess(MiddleResult middleResult)
        {
            if (!middleResult.ErrorNo.Equals("0"))
            {
                throw new FrameException(middleResult.ErrorNo, middleResult.ErrorInfo);
            }
        }
    }
}
