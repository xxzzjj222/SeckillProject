using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Caches
{
    public static class RedisServiceCollectionExtensions
    {
        /// <summary>
        /// 注册分布式Redis缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services,string connectionString)
        {
            //创建CSRedis客户端
            var csredis = new CSRedisClient(connectionString);
            //加入到容器
            services.AddSingleton(csredis);
            //初始化
            RedisHelper.Initialization(csredis);

            return services;
        }


        /// <summary>
        ///  注册分布式Redis集群缓存
        /// </summary>
        /// <typeparam name="connectionString"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, string[] connectionString)
        {
            // 1、创建redis客户端实例
            var csredis = new CSRedisClient((d) => { return ""; }, connectionString);

            // 2、注册RedisClient到IOC
            services.AddSingleton(csredis);

            // 3、添加到redi帮助类
            RedisHelper.Initialization(csredis);//初始化
            return services;
        }
    }
}
