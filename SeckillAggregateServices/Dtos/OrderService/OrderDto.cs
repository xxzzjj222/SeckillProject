using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Dtos.OrderService
{
    /// <summary>
    /// 预订单模型
    /// </summary>
    public class OrderDto
    {
        public int Id { set; get; } // 订单主键
        public string OrderType { set; get; } // 订单类型
        public string OrderFlag { set; get; } // 订单标志
        public int UserId { set; get; } // 用户Id
        public string OrderSn { set; get; }// 订单号
        public decimal OrderTotalPrice { set; get; } // 订单总价
        public DateTime Createtime { set; get; } // 创建时间
        public DateTime Updatetime { set; get; } // 更新时间
        public DateTime Paytime { set; get; }// 支付时间
        public DateTime Sendtime { set; get; }// 发货时间
        public DateTime Successtime { set; get; }// 订单完成时间
        public int OrderStatus { set; get; } // 订单状态
        public string OrderName { set; get; } // 订单名称
        public string OrderTel { set; get; } // 订单电话
        public string OrderAddress { set; get; } // 订单地址
        public string OrderRemark { set; get; }// 订单备注

        // 订单项Dto
        public List<OrderItemDto> OrderItemDtos { set; get; }
    }
}
