using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Caches.SeckillStock
{
    /// <summary>
    /// 秒杀缓存接口
    /// </summary>
    public interface ISeckillStockCache
    {
        /// <summary>
        /// 秒杀库存加载到缓存
        /// </summary>
        public void SeckillStockToCache();

        /// <summary>
        /// 根据商品编号获取秒杀库存
        /// </summary>
        /// <returns></returns>
        public int GetSeckillStock(int productId);
        
        /// <summary>
        /// 扣减秒杀库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productCount"></param>
        public void SubstrackSeckillStock(int productId, int productCount);
    }
}
