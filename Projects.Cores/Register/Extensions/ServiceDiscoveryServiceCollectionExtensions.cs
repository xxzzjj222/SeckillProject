using Microsoft.Extensions.DependencyInjection;
using Projects.Cores.Register.Consul;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Register.Extensions
{
    /// <summary>
    /// 服务发现ioc容器扩展
    /// </summary>
    public static class ServiceDiscoveryServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            AddServiceDiscovery(services, options => { });

            return services;
        }

        /// <summary>
        /// consul服务发现
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services,Action<ServiceDiscoveryOptions> options)
        {
            //1.注册配置选项
            services.Configure(options);

            //2.注册consul服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();

            return services;
        }
    }
}
