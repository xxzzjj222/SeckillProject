using Projects.Cores.Middleware.Options;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware.Options
{
    public class DynamicMiddlewareOptions
    {
        /// <summary>
        /// 服务发现选项
        /// </summary>
        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { get; set; }

        /// <summary>
        /// 中台选项
        /// </summary>
        public Action<MiddlewareOptions> middelwareOptions { get; set; }
    }
}
