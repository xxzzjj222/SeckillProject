using Projects.Common.Exceptions;
using SeckillAggregateServices.Models.SeckillService;
using SeckillAggregateServices.Services.SeckillService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Caches.SeckillStock
{
    public class RedisSeckillStockCache : ISeckillStockCache
    {
        private readonly ISeckillsClient _seckillsClient;

        public RedisSeckillStockCache(ISeckillsClient seckillsClient)
        {
            _seckillsClient = seckillsClient;
        }

        public int GetSeckillStock(int productId)
        {
            return Convert.ToInt32(RedisHelper.HGet(Convert.ToString(productId), "SeckillStock"));
        }

        public void SeckillStockToCache()
        {
            List<Seckill> seckills = _seckillsClient.GetSeckills();

            foreach (var seckill in seckills)
            {
                bool flag1=RedisHelper.HSet(Convert.ToString(seckill.ProductId), "SeckillStock", seckill.SeckillStock);
                bool flag2=RedisHelper.HSet(Convert.ToString(seckill.ProductId), "SeckillLimit", seckill.SeckillLimit);

                //if(flag1 || flag2)
                //{
                //    throw new BizException("redis存储数据失败");
                //}
            }
        }

        public void SubstrackSeckillStock(int productId, int productCount)
        {
            long seckillStock = RedisHelper.HIncrBy(Convert.ToString(productId), "SeckillStock", -productCount);
            
            if(seckillStock<0)
            {
                throw new BizException("秒杀已结束");
            }
        }
    }
}
