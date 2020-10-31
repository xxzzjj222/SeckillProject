using Projects.Cores.Pollys.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Middleware.Options
{
    /// <summary>
    /// 中台配置选项
    /// </summary>
    public class MiddlewareOptions
    {
        public MiddlewareOptions()
        {
            HttpClientName = "Micro";
            pollyHttpClientOptions = options => { };
        }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string HttpClientName { get; set; }

        //polly熔断降级选项
        public Action<PollyHttpClientOptions> pollyHttpClientOptions { get; }
    }
}
