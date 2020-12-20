using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Pos.PaymentService
{
    /// <summary>
    /// 支付参数模型
    /// </summary>
    public class PaymentPo
    {
        public int UserId { set; get; } // 用户编号
        public int OrderId { set; get; } // 订单主键
        public int PaymentType { set; get; } // 支付类型
        public string OrderSn { set; get; } // 订单号
        public decimal OrderTotalPrice { set; get; } // 总金额
    }
}
