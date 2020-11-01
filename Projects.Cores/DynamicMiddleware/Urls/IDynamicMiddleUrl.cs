using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware.Urls
{
    public interface IDynamicMiddleUrl
    {
        /// <summary>
        /// 获取中台url
        /// </summary>
        /// <param name="urlScheme">中台url</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public string GetMiddleUrl(string urlScheme, string serviceName);
    }
}
