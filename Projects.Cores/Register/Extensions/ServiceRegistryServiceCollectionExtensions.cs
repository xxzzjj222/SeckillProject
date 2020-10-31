using Microsoft.Extensions.DependencyInjection;
using Projects.Cores.Register.Consul;
using Projects.Cores.Register.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Projects.Cores.Register.Extensions
{
    /// <summary>
    /// 服务注册ioc容器扩展
    /// </summary>
    public static class ServiceRegistryServiceCollectionExtensions
    {
        /// <summary>
        /// consul服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            AddServiceRegistry(services, options => { });

            return services;
        }
        /// <summary>
        /// consul服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services,Action<ServiceRegistryOptions> options)
        {
            //1.配置选项到IOC
            services.Configure(options);

            //2.注册consul注册
            services.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();

            //3.注册开启自动注册服务
            services.AddHostedService<ServiceRegistryIHostedService>();

            return services;
        }
    }
}
