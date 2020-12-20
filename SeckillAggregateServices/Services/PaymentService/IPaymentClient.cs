using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.PaymentService
{
    /// <summary>
    /// 支付微服务客户端
    /// </summary>
    [MicroClient("https", "PaymentServices")]
    public interface IPaymentClient
    {
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="payment"></param>
        [PostPath("/Payments")]
        public Payment Pay(Payment payment);
    }
}
