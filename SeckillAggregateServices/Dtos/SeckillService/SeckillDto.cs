using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Dtos.SeckillService
{
    /// <summary>
    /// 秒杀活动Dto
    /// </summary>
    public class SeckillDto
    {
        public int Id { set; get; } // 秒杀编号
        public int SeckillType { set; get; } // 秒杀类型
        public string SeckillName { set; get; } // 秒杀名称
        public string SeckillUrl { set; get; } // 秒杀URL
        public decimal SeckillPrice { set; get; } // 秒杀价格
        public int SeckillStock { set; get; } // 秒杀库存
        public int SeckillSum { set; get; } // 秒杀数量(已经秒杀的数量)
        public string SeckillPercent { set; get; } // 秒杀百分比
        public int TimeId { set; get; } // 秒杀时间编号
        public int ProductId { set; get; } // 商品编号
        public int SeckillLimit { set; get; } // 秒杀限制数量
        public string SeckillDescription { set; get; } // 秒杀描述
        public int SeckillIstop { set; get; } // 秒杀是否结束
        public int SeckillStatus { set; get; } // 秒杀状态
        public decimal ProductPrice { set; get; } // 商品价格
        public string ProductDescription { set; get; }// 商品描述
        public string ProductTitle { set; get; }// 商品名称
        public string ProductUrl { set; get; }// 商品URL
        
        // public int ProductStock { set; get; } // 商品库存
    }
}
