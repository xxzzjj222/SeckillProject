using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Dtos.ProductService
{
    /// <summary>
    /// 商品Dto
    /// </summary>
    public class ProdcutDto
    {
        public int ProductId { set; get; } // 商品编号
        public int ProductCount { set; get; }// 商品数量
        public decimal ProductPrice { set; get; } // 订单价格
        public string ProductUrl { set; get; } // 商品图片
        public int UserId { set; get; } // 用户Id
    }
}
