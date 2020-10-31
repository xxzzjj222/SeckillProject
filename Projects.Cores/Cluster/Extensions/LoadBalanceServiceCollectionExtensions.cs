using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Projects.Cores.Cluster.Options;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Projects.Cores.Cluster.Extensions
{
    /// <summary>
    /// 负载均衡serviceCollection扩展
    /// </summary>
    public static class LoadBalanceServiceCollectionExtensions
    {
        /// <summary>
        /// 注册负载均衡
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoadBalance(this IServiceCollection services)
        {
            AddLoadBalance(services, options => { });
            return services;
        }

        /// <summary>
        /// 注册负载均衡
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoadBalance(this IServiceCollection services,Action<LoadBalanceOptions> action)
        {
            var options = new LoadBalanceOptions();
            action(options);

            //services.Configure(action);

            var assembly = typeof(LoadBalanceServiceCollectionExtensions).Assembly;
            Type type = assembly.GetType("Projects.Cores.Cluster." + options.Type + "LoadBalance");
            if (type!=null)
            {
                services.AddSingleton((ILoadBalance)assembly.CreateInstance(type.FullName));
            }

            return services;
        }
    }
}
