using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.OrderService
{
    /// <summary>
    /// 订单微服务客户端
    /// </summary>
    [MicroClient("http", "OrderServices")]
    public interface IOrderClient
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        [PostPath("/Orders")]
        public Order CreateOrder(Order order);
    }
}
