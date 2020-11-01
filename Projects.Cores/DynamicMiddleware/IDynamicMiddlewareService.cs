using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware
{
    public interface IDynamicMiddlewareService
    {
        public IList<IDictionary<string, object>> GetList(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public IDictionary<string, object> Get(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public dynamic GetDynamic(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public IList<T> GetList<T>(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new();

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public T Get<T>(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new();

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Post(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Post请求，动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic PostDynamic(string urlScheme, string serviceName, string serviceLink, dynamic middleParam);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台集合参数</param>
        /// <returns></returns>
        public void Post(string urlScheme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams);


        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Delete(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic DeleteDynamic(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Put(string urlScheme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求，动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic PutDynamic(string urlScheme, string serviceName, string serviceLink, dynamic middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台参数</param>
        /// <returns></returns>
        public void Put(string urlScheme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams);
    }
}
