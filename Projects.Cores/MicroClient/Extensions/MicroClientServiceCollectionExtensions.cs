using Microsoft.Extensions.DependencyInjection;
using Projects.Cores.DynamicMiddleware;
using Projects.Cores.DynamicMiddleware.Extensions;
using Projects.Cores.MicroClient.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.MicroClient.Extensions
{
    public static class MicroClientServiceCollectionExtensions
    {
        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services,string assemblyName)
        {
            //注册动态中台
            services.AddDynamicMiddleware<IDynamicMiddlewareService, DefaultDynamicMiddleService>();
            //注册动态代理工厂
            services.AddSingleton<MicroClientProxyFactory>();
            //注册客户端
            services.AddSingleton<MicroClientList>();
            //注册客户端代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClientList = serviceProvider.GetService<MicroClientList>();
            IDictionary<Type, object> dics = microClientList.GetClients(assemblyName);

            foreach(var type in dics.Keys)
            {
                services.AddSingleton(type, dics[type]);
            }
            return services;
        }

        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services,Action<MicroClientOptions> options)
        {
            MicroClientOptions microClientOptions = new MicroClientOptions();
            options(microClientOptions);

            // 1、注册AddMiddleware
            services.AddDynamicMiddleware<IDynamicMiddlewareService, DefaultDynamicMiddleService>(microClientOptions.dynamicMiddlewareOptions);

            // 2、注册客户端工厂
            services.AddSingleton<MicroClientProxyFactory>();

            // 3、注册客户端集合
            services.AddSingleton<MicroClientList>();

            // 4、注册代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClients = serviceProvider.GetRequiredService<MicroClientList>();

            IDictionary<Type, object> dics = microClients.GetClients(microClientOptions.AssemblyName);
            foreach (var key in dics.Keys)
            {
                services.AddSingleton(key, dics[key]);
            }
            return services;
        }
    }
}
