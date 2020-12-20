using Microsoft.Extensions.Caching.Memory;
using SeckillAggregateServices.Models.SeckillService;
using SeckillAggregateServices.Services.SeckillService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Caches.SeckillStock
{
    public class SeckillStockCache : ISeckillStockCache
    {
        private readonly ISeckillsClient _seckillsClient;

        private readonly IMemoryCache _memoryCache;

        public SeckillStockCache(ISeckillsClient seckillsClient,IMemoryCache memoryCache)
        {
            _seckillsClient = seckillsClient;
            _memoryCache = memoryCache;
        }
        public int GetSeckillStock(int productId)
        {
            Seckill seckill = _memoryCache.Get<Seckill>(productId);
            return seckill.SeckillStock;
        }

        /// <summary>
        /// 秒杀库存加载到MemoryCache中
        /// </summary>
        public void SeckillStockToCache()
        {
            // 1、查询所有秒杀活动
            List<Seckill> seckills = _seckillsClient.GetSeckills();

            // 2、存储秒杀库存到缓存
            foreach (var seckill in seckills)
            {
                _memoryCache.Set<Seckill>(seckill.ProductId, seckill);
            }
        }

        public void SubstrackSeckillStock(int productId, int productCount)
        {
            ///获取秒杀信息
            Seckill seckill = _memoryCache.Get<Seckill>(productId);

            //扣减库存
            seckill.SeckillStock = seckill.SeckillStock = productCount;


            //更新库存
            _memoryCache.Set<Seckill>(productId, seckill);
        }
    }
}
