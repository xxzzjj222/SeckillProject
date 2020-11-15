using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Distributes
{
    public static class DistributedOrderSnServiceCollectionExtensions
    {
        /// <summary>
        ///  注册分布式Redis集群缓存
        /// </summary>
        /// <typeparam name="connectionString"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddDistributedOrderSn(this IServiceCollection services, long datacenterId, long workerId)
        {
            // 1、注册雪花Id
            SnowflakeId snowflakeId = new SnowflakeId(datacenterId, workerId);
            services.AddSingleton(snowflakeId);

            // 2、注册分布式订单号
            services.AddSingleton<DistributedOrderSn>();
            return services;
        }
    }
}
