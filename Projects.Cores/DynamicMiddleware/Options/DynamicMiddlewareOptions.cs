using Projects.Cores.Cluster.Options;
using Projects.Cores.Middleware.Options;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware.Options
{
    public class DynamicMiddlewareOptions
    {
        public DynamicMiddlewareOptions()
        {
            serviceDiscoveryOptions = options => { };
            middelwareOptions = options => { };
            loadBalanceOptions = options => { };
        }

        /// <summary>
        /// 服务发现选项
        /// </summary>
        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { get; set; }

        /// <summary>
        /// 中台选项
        /// </summary>
        public Action<MiddlewareOptions> middelwareOptions { get; set; }

        /// <summary>
        /// 负载均衡选项
        /// </summary>
        public Action<LoadBalanceOptions> loadBalanceOptions { get; set; }
    }
}
