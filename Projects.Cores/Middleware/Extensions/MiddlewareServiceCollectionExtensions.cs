using Microsoft.Extensions.DependencyInjection;
using Projects.Cores.Middleware.Options;
using Projects.Cores.Pollys.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Middleware.Extensions
{
    /// <summary>
    /// 中台ServicesCollection扩展方法
    /// </summary>
    public static class MiddlewareServiceCollectionExtensions
    {
        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            AddMiddleware(services, options => { });
            return services;
        }

        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiddleware(this IServiceCollection services,Action<MiddlewareOptions> options)
        {
            MiddlewareOptions middlewareOptions = new MiddlewareOptions();
            options(middlewareOptions);

            //1.注册到IOC
            services.Configure<MiddlewareOptions>(options);

            //2.添加httpclient
            services.AddPollyHttpClient(middlewareOptions.HttpClientName, middlewareOptions.pollyHttpClientOptions);

            //3.注册中台
            services.AddSingleton<IMiddleService, HttpMiddleService>();

            return services;
        }
    }
}
