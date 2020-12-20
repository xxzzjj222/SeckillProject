using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.SeckillService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.SeckillService
{
    /// <summary>
    /// 秒杀记录客户端
    /// </summary>
    [MicroClient("http", "SeckillServices")]
    public interface ISeckillTimeClient
    {
        /// <summary>
        /// 查询秒杀时间表
        /// </summary>
        /// <returns></returns>
        [GetPath("/SeckillTimeModels")]
        public List<SeckillTimeModel> GetSeckillTimes();

        /// <summary>
        /// 根据时间查询秒杀活动
        /// </summary>
        /// <returns></returns>
        [GetPath("/SeckillTimeModels/{timeId}/Seckills")]
        public List<Seckill> GetSeckills([PathVariable("timeId")] int timeId);
    }
}
