using Projects.Cores.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.SeckillService
{
    /// <summary>
    /// 秒杀记录客户端
    /// </summary>
    public interface ISeckillRecordClient
    {
        /// <summary>
        /// 查询秒杀活动列表
        /// </summary>
        /// <returns></returns>
        public MiddleResult GetSeckillList();
    }
}
