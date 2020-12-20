using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Caches.SeckillStock
{
    /// <summary>
    /// 服务启动时，加载秒杀库存到缓存
    /// </summary>
    public class SeckillStockCacheHostedService : IHostedService
    {
        private readonly ISeckillStockCache _seckillStockCache;

        public SeckillStockCacheHostedService(ISeckillStockCache seckillStockCache)
        {
            _seckillStockCache = seckillStockCache;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("加载秒杀库存到缓存中");
            return Task.Run(() => _seckillStockCache.SeckillStockToCache());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
