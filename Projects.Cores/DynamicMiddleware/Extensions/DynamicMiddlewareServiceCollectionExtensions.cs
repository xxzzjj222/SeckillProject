using Microsoft.Extensions.DependencyInjection;
using Projects.Cores.Cluster.Extensions;
using Projects.Cores.DynamicMiddleware.Options;
using Projects.Cores.DynamicMiddleware.Urls;
using Projects.Cores.Middleware.Extensions;
using Projects.Cores.Register.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.DynamicMiddleware.Extensions
{
    public static class DynamicMiddlewareServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicMiddleware<TMiddleService,TMiddleImplementation>(this IServiceCollection services)
            where TMiddleService:class
            where TMiddleImplementation:class,TMiddleService
        {
            AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(services, options => { });
            return services;
        }

        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services,Action<DynamicMiddlewareOptions> action)
           where TMiddleService : class
           where TMiddleImplementation : class, TMiddleService
        {
            var options = new DynamicMiddlewareOptions();
            action(options);

            //注册服务发现
            services.AddServiceDiscovery(options.serviceDiscoveryOptions);

            //注册负载均衡
            services.AddLoadBalance(options.loadBalanceOptions);

            //注册中台组件
            services.AddMiddleware(options.middelwareOptions);

            //注册动态中台url服务
            services.AddSingleton<IDynamicMiddleUrl, DefaultDynamicMiddleUrl>();

            //注册动态中台
            services.AddSingleton<TMiddleService, TMiddleImplementation>();

            return services;

        }
    }
}
