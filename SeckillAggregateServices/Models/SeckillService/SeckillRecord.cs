using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Models.SeckillService
{
    /// <summary>
    /// 秒杀记录模型
    /// </summary>
    public class SeckillRecord
    {
        [Key]
        public int Id { set; get; }            // 秒杀记录编号
        public DateTime Createtime { set; get; }      // 秒杀记录创建时间
        public DateTime Updatetime { set; get; }      // 秒杀记录更新时间
        public decimal RecordTotalprice { set; get; }        // 秒杀记录总价
        public int SeckillId { set; get; }               // 秒杀活动编号
        public int SeckillNum { set; get; }              // 秒杀数量
        public decimal SeckillPrice { set; get; }            // 秒杀价格
        public decimal ProductPrice { set; get; }            // 商品原价
        public int OrderId { set; get; }                 // 订单编号
        public int UserId { set; get; }                  // 用户编号
        public int RecordStatus { set; get; }            // 秒杀记录状态
    }
}
