using Consul;
using Microsoft.Extensions.Options;
using Projects.Cores.Exceptions;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Projects.Cores.Register.Consul
{
    public class ConsulServiceDiscovery : AbstractServiceDiscovery
    {
        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryOptions> options) : base(options)
        {
        }

        protected override CatalogService[] RemoteDiscovery(string serviceName)
        {
            //1.创建客户端连接2s 1、使用单例全局共享 2、使用数据缓存(进程：字典，集合) 3、使用连接池
            var consulClient = new ConsulClient(config =>
              {
                  config.Address = new Uri(_serviceDiscoveryOptions.DiscoveryAddress);
              });

            //2.根据名称获取服务
            var queryResult = consulClient.Catalog.Service(serviceName).Result;

            //3.判断请求是否失败
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameException($"consul连接失败:{queryResult.StatusCode}");
            }
            return queryResult.Response;
        }
    }
}
