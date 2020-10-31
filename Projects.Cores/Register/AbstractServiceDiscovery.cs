using Consul;
using Microsoft.Extensions.Options;
using Projects.Cores.Exceptions;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Projects.Cores.Register
{
    /// <summary>
    /// 抽象服务发现，主要是缓存功能
    /// </summary>
    public abstract class AbstractServiceDiscovery : IServiceDiscovery
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private readonly Dictionary<string, List<ServiceNode>> CacheConsulResult = new Dictionary<string, List<ServiceNode>>();
        protected readonly ServiceDiscoveryOptions _serviceDiscoveryOptions;

        public AbstractServiceDiscovery(IOptions<ServiceDiscoveryOptions> options)
        {
            this._serviceDiscoveryOptions = options.Value;

            //1.创建consul客户端
            var consulClient = new ConsulClient(config =>
              {
                  config.Address = new Uri(_serviceDiscoveryOptions.DiscoveryAddress);
              });

            //2.consul先查询服务
            var queryResult = consulClient.Catalog.Services().Result;
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameException($"consul连接失败:{queryResult.StatusCode}");
            }

            //3.获取服务下的所有实例
            foreach (var item in queryResult.Response)
            {
                QueryResult<CatalogService[]> result = consulClient.Catalog.Service(item.Key).Result;
                if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw new FrameException($"consul连接失败:{queryResult.StatusCode}");
                }
                var list = new List<ServiceNode>();
                foreach (var service in result.Response)
                {
                    list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
                }
                CacheConsulResult.Add(item.Key, list);
            }
        }
        public List<ServiceNode> Discovery(string serviceName)
        {
            //1.从缓存中查询consul结果
            if (CacheConsulResult.ContainsKey(serviceName))
            {
                return CacheConsulResult[serviceName];
            }
            else
            {
                //2.从远程服务获取
                CatalogService[] queryResult = RemoteDiscovery(serviceName);

                var list = new List<ServiceNode>();
                foreach (var service in queryResult)
                {
                    list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
                }
                //存入缓存
                CacheConsulResult.Add(serviceName, list);
                return list;
            }
        }

        /// <summary>
        /// 远程服务发现
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        protected abstract CatalogService[] RemoteDiscovery(string serviceName);
    }
}
