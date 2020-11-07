using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Attributes
{
    /// <summary>
    /// 微服务客户端特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class MicroClient:Attribute
    {
        public MicroClient(string urlScheme,string serviceName)
        {
            UrlScheme = urlScheme;
            ServiceName = serviceName;
        }
        public string UrlScheme { get;set; }

        public string ServiceName { get; set; }
    }
}
