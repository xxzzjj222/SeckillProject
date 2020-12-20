using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Caches.SeckillStock
{
    /// <summary>
    /// 秒杀库存使用IOC容器触发
    /// </summary>
    public static class SeckillStockCacheServiceCollectionExtensions
    {
        /// <summary>
        /// 添加秒杀库存到memoryCache
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSeckillStockCache(this IServiceCollection services)
        {
            services.AddSingleton<ISeckillStockCache, SeckillStockCache>();

            services.AddHostedService<SeckillStockCacheHostedService>();

            return services;
        }

        /// <summary>
        /// 添加秒杀库存到redis
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisSeckillStockCache(this IServiceCollection services)
        {
            services.AddSingleton<ISeckillStockCache, RedisSeckillStockCache>();

            services.AddHostedService<SeckillStockCacheHostedService>();

            return services;
        }
    }
}
