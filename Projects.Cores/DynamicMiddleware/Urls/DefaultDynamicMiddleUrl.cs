using Projects.Cores.Cluster;
using Projects.Cores.Exceptions;
using Projects.Cores.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware.Urls
{
    public class DefaultDynamicMiddleUrl : IDynamicMiddleUrl
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly ILoadBalance _loadBalance;

        public DefaultDynamicMiddleUrl(IServiceDiscovery serviceDiscovery,ILoadBalance loadBalance)
        {
            _serviceDiscovery = serviceDiscovery;
            _loadBalance = loadBalance;
        }
        public string GetMiddleUrl(string urlScheme, string serviceName)
        {
            //1.获取服务url
            IList<ServiceNode> serviceNodes = _serviceDiscovery.Discovery(serviceName);
            if (serviceNodes.Count==0)
            {
                throw new FrameException($"{serviceName} 服务不存在");
            }

            //2.url负载均衡
            var serviceNode = _loadBalance.Select(serviceNodes);

            return urlScheme + "://" + serviceNode.Url;
        }
    }
}
