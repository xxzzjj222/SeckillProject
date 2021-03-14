using AutoMapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Projects.Common.Distributes;
using Projects.Common.Exceptions;
using Projects.Common.Users;
using Projects.Common.Utils;
using SeckillAggregateServices.Caches.SeckillStock;
using SeckillAggregateServices.Dtos.OrderService;
using SeckillAggregateServices.Dtos.PaymentServcie;
using SeckillAggregateServices.Models.OrderService;
using SeckillAggregateServices.Pos.OrderService;
using SeckillAggregateServices.Pos.ProductService;
using SeckillAggregateServices.Services.OrderService;
using SeckillAggregateServices.Services.SeckillService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 订单聚合控制器
    /// 从粗取精  去伪存真  由此及彼  由表及里 
    /// while
    /// 抽象目的 帮助我们形成现象 20个属性 18 ------ 概念 class 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderClient orderClient;
        private readonly ISeckillsClient seckillsClient;
        private readonly IMemoryCache memoryCache;

        private readonly ISeckillStockCache seckillStockCache;
        private readonly ICapPublisher capPublisher;
        private readonly DistributedOrderSn distributedOrderSn;

        public OrderController(IOrderClient orderClient,
                              ISeckillsClient seckillsClient,
                              IMemoryCache memoryCache,
                              ISeckillStockCache seckillStockCache,
                              ICapPublisher capPublisher,
                              DistributedOrderSn distributedOrderSn)
        {
            this.orderClient = orderClient;
            this.seckillsClient = seckillsClient;
            this.memoryCache = memoryCache;
            this.seckillStockCache = seckillStockCache;
            this.capPublisher = capPublisher;
            this.distributedOrderSn = distributedOrderSn;
        }

        
        /// <summary>
        /// 创建预订单
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="productPo"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public OrderDto CreatePreOrder(SysUser sysUser,[FromForm]ProductPo productPo)
        {
            //1.创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            //2.计算总价
            decimal itemTotalPrice = productPo.ProductCount * productPo.ProductPrice;

            //3.创建订单项
            OrderItemDto orderItemDto = new OrderItemDto();
            orderItemDto.OrderSn = orderSn;
            orderItemDto.ProductId = productPo.ProductId;
            orderItemDto.ItemCount = productPo.ProductCount;
            orderItemDto.ItemPrice = productPo.ProductPrice;
            orderItemDto.ItemTotalPrice = itemTotalPrice;

            //4.创建订单
            OrderDto orderDto = new OrderDto();
            orderDto.UserId = sysUser.UserId;
            orderDto.OrderItemDtos = new List<OrderItemDto>()
            {
                orderItemDto
            };
            return orderDto;
        }
        
        /// <summary>
        /// 1.创建订单
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="orderPo"></param>
        /// <returns></returns>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser, [FromForm] OrderPo orderPo)
        {
            //1.创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            //2.扣减库存
            seckillsClient.SeckillSetStock(orderPo.ProductId, orderPo.ProductCount);

            //3.设置订单
            var configuration = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<OrderPo, Order>();
              });
            IMapper mapper = configuration.CreateMapper();
            Order order = mapper.Map<OrderPo, Order>(orderPo);
            order.OrderSn = orderSn;
            order.UserId = sysUser.UserId;

            //4.设置订单项
            OrderItem orderItem = new OrderItem();
            orderItem.ItemCount = orderPo.ProductCount;
            orderItem.ItemPrice = orderPo.OrderTotalPrice;
            orderItem.ItemTotalPrice = orderPo.OrderTotalPrice;
            orderItem.ProductUrl = orderPo.ProductUrl;
            orderItem.ProductId = orderPo.ProductId;
            orderItem.OrderSn = orderSn;

            order.OrderItems = new List<OrderItem>() { orderItem };

            //5.保存订单
            order = orderClient.CreateOrder(order);

            //6.创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;

            return paymentDto;
        }

        /*
        /// <summary>
        /// 2、创建订单(缓存库存扣减)
        /// 170 并发
        /// 1700 并发
        /// </summary>
        /// <param name="orderDto"></param>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser,[FromForm] OrderPo orderPo)
        {
            // 1、创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            // 2、扣减库存(从缓存扣取)
            seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            // 3、设置订单
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderPo, Order>();
            });

            IMapper mapper = configuration.CreateMapper();
            Order order = mapper.Map<OrderPo, Order>(orderPo);
            order.OrderSn = orderSn;
            order.UserId = sysUser.UserId;

            // 4、设置订单项
            OrderItem orderItem = new OrderItem();
            orderItem.ItemCount = orderPo.ProductCount;
            orderItem.ItemPrice = orderPo.OrderTotalPrice;
            orderItem.ItemTotalPrice = orderPo.OrderTotalPrice;
            orderItem.ProductUrl = orderPo.ProductUrl;
            orderItem.ProductId = orderPo.ProductId;
            orderItem.OrderSn = orderSn;

            List<OrderItem> orderItems = new List<OrderItem>();
            orderItems.Add(orderItem);
            order.OrderItems = orderItems;

            // 5、保存订单
            order = orderClient.CreateOrder(order);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = order.OrderSn;
            paymentDto.OrderTotalPrice = order.OrderTotalPrice;
            paymentDto.OrderId = order.Id;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /*
        /// <summary>
        /// 3.创建订单（缓存扣减库存+消息队列）
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="orderPo"></param>
        /// <returns></returns>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser,[FromForm]OrderPo orderPo)
        {
            //1.创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            //2.扣减库存(缓存）
            seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            //3.发送消息到rabbitmq
            SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /*
        /// <summary>
        /// 4、创建订单(redis扣减库存 + 消息队列)
        /// </summary>
        /// <param name="orderPo"></param>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser, [FromForm]OrderPo orderPo)
        {
            // 1、创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            // 2、扣减库存(redis缓存+redis扣减)
            seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            // 3、发送订单消息到rabbitmq
            SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /*
        /// <summary>
        /// 5、创建订单(redis扣减库存 + 消息队列 + 单品限流)
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="orderPo"></param>
        /// <returns></returns>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser,[FromForm]OrderPo orderPo)
        {
            // 1、单品限流(限制单品在有限时间内请求次数)
            string LimitKey = "SeckillRequestLimit" + orderPo.ProductId;
            string LimitCount = RedisHelper.Get(LimitKey);
            int SeckillRequestLimitCount = string.IsNullOrEmpty(LimitCount) ? 0 : Convert.ToInt32(LimitCount);
            int RequestLimits = 100; // 限制请求数量
            int expireSeconds = 1; // 1秒
            if (SeckillRequestLimitCount + 1 > RequestLimits)
            {
                // 1.1、抛出异常，达到最高限制数
                throw new BizException($"商品{orderPo.ProductName}:{expireSeconds}秒内只能请求{RequestLimits}次");
            }
            else
            {
                // 1.2、增加1 并设置2秒过期
                RedisHelper.IncrBy(LimitKey, 1);
                RedisHelper.Expire(LimitKey, expireSeconds);
            }

            // 1、创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            // 2、扣减库存(redis缓存+redis扣减)
            seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            // 3、发送订单消息到rabbitmq
            SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /*
        /// <summary>
         /// 6、创建订单(redis扣减库存 + 消息队列 + 单品限流 + 限制购买数量)
         /// </summary>
         /// <param name="orderDto"></param>
         [HttpPost]
         public PaymentDto CreateOrder(SysUser sysUser, [FromForm]OrderPo orderPo)
         {
             #region 1、单品限流
             {
                 // 1、单品限流(限制单品在有限时间内请求次数)
                 string LimitKey = "SeckillRequestLimit" + orderPo.ProductCount;
                 string LimitCount = RedisHelper.Get(LimitKey);
                 int SeckillRequestLimitCount = string.IsNullOrEmpty(LimitCount) ? 0 : Convert.ToInt32(LimitCount);
                 int RequestLimits = 100; // 限制请求数量
                 int expireSeconds = 10; // 2秒
                 if (SeckillRequestLimitCount + 1 > RequestLimits)
                 {
                     // 1.1、抛出异常，达到最高限制数
                     throw new BizException($"{expireSeconds}秒内只能请求{RequestLimits}次");
                 }
                 else
                 {
                     // 1.2、增加1 并设置2秒过期
                     RedisHelper.IncrBy(LimitKey, 1);
                     RedisHelper.Expire(LimitKey, expireSeconds);
                 }
             }
             #endregion

             #region 2、限制购买数量
             {
                 // 2、限制购买数量(防止刷单)
                 string UserBuyLimitKey = "UserId" + sysUser.UserId + "ProductId" + orderPo.ProductId;
                 string count = RedisHelper.HGet(UserBuyLimitKey, "UserBuyLimit");
                 int intCount = string.IsNullOrEmpty(count) ? 0 : Convert.ToInt32(count);

                 // 2.1 获取秒杀活动数量
                 string SeckillLimit = RedisHelper.HGet(Convert.ToString(orderPo.ProductId), "SeckillLimit");
                 int SeckillLimitCount = string.IsNullOrEmpty(SeckillLimit) ? 0 : Convert.ToInt32(SeckillLimit);
                 if (intCount + 1 > SeckillLimitCount)
                 {
                     throw new BizException($"商品{orderPo.ProductName}:只能购买{SeckillLimit}次");
                 }
                 else
                 {
                     // 2.2 增加用户购买数量
                     RedisHelper.HIncrBy(UserBuyLimitKey, "UserBuyLimit", orderPo.ProductCount);
                 }
             }
             #endregion

             // 1、创建订单号
             string orderSn = OrderUtil.GetOrderCode();

             // 2、扣减库存(redis缓存+redis扣减)
             seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

             // 3、发送订单消息到rabbitmq
             SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

             // 6、创建支付信息
             PaymentDto paymentDto = new PaymentDto();
             paymentDto.OrderSn = orderSn;
             paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
             paymentDto.UserId = sysUser.UserId;
             paymentDto.ProductId = orderPo.ProductId;
             paymentDto.ProductName = orderPo.ProductName;

             return paymentDto;
         }*/

        /*
        /// <summary>
        /// 4.3、创建订单(redis + 消息队列 + lua)
        /// </summary>
        /// <param name="orderDto"></param>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser, [FromForm] OrderPo orderPo)
        {

            //RedisHelper.Eval("SeckillLua.lua","222","3222","2222");
            //RedisHelper.EvalSHA("dddddd", "22222222222");// key = "22222222222"; // 内存缓存MemroyCache
            //RedisHelper.ScriptLoad
                                    // 1、redis秒杀开始
            string ProductKey = Convert.ToString(orderPo.ProductId);// 商品key
            string SeckillLimitKey = "seckill_stock_:SeckillLimit" + orderPo.ProductCount; // 单品限流key
            string UserBuyLimitKey = "seckill_stock_:UserId" + sysUser.UserId + "ProductId" + orderPo.ProductId;// 用户购买限制key
            int productCount = orderPo.ProductCount; // 购买商品数量 2
            int requestCountLimits = 100; // 单品限流数量
            int seckillLimitKeyExpire = 1;// 单品限流时间
            var SeckillResult = RedisHelper.EvalSHA(memoryCache.Get<string>("luaSha"), ProductKey, UserBuyLimitKey, SeckillLimitKey, productCount, requestCountLimits, seckillLimitKeyExpire);
            if (!SeckillResult.ToString().Equals("1"))
            {
                throw new BizException(SeckillResult.ToString());
            }

            // 1、创建订单号
            string orderSn = OrderUtil.GetOrderCode();

            // 2、扣减库存(redis缓存+redis扣减)
            seckillStockCache.SubstrackSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            // 3、发送订单消息到rabbitmq
            SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }
        */

        /*
        /// <summary>
        /// 4.4、创建订单(redis + 消息队列 + lua + 方法幂等)
        /// </summary>
        /// <param name="orderDto"></param>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser, [FromForm]OrderPo orderPo)
        {
            // 1、秒杀参数准备
            string ProductKey = Convert.ToString(orderPo.ProductId);// 商品key
            string SeckillLimitKey = "seckill_stock_:SeckillLimit" + orderPo.ProductCount; // 单品限流key
            string UserBuyLimitKey = "seckill_stock_:UserId" + sysUser.UserId + "ProductId" + orderPo.ProductId;// 用户购买限制key
            int productCount = orderPo.ProductCount; // 购买商品数量
            int requestCountLimits = 10; // 单品限流数量
            int seckillLimitKeyExpire = 10;// 单品限流时间
            string requestIdKey = "seckill_stock_:" + orderPo.RequestId; // requestIdKey
            string orderSn = OrderUtil.GetOrderCode();// 订单号

            // 2、执行
            var SeckillResult = RedisHelper.EvalSHA(memoryCache.Get<string>("luaSha"), ProductKey, UserBuyLimitKey, SeckillLimitKey, productCount, requestCountLimits, seckillLimitKeyExpire, requestIdKey, orderSn);
            if (!SeckillResult.ToString().Equals("1"))
            {
                throw new BizException(SeckillResult.ToString());
            }

            // 2、扣减库存(redis缓存+redis扣减)
            //seckillStockCache.SubtractSeckillStock(orderPo.ProductId, orderPo.ProductCount);

            // 3、发送订单消息到rabbitmq
            SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);

            // 6、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /*
        /// <summary>
        /// 4.5、创建订单(redis + 消息队列 + lua + 方法幂等 + 失败回滚)
        /// </summary>
        /// <param name="orderDto"></param>
        [HttpPost]
        public PaymentDto CreateOrder(SysUser sysUser, [FromForm]OrderPo orderPo)
        {
            // 1、秒杀参数准备
            string ProductKey = Convert.ToString(orderPo.ProductId);// 商品key
            string SeckillLimitKey = "seckill_stock_:SeckillLimit" + orderPo.ProductCount; // 单品限流key
            string UserBuyLimitKey = "seckill_stock_:UserId" + sysUser.UserId + "ProductId" + orderPo.ProductId;// 用户购买限制key
            int productCount = orderPo.ProductCount; // 购买商品数量
            int requestCountLimits = 60000; // 单品限流数量
            int seckillLimitKeyExpire = 60;// 单品限流时间：单位秒
            string requestIdKey = "seckill_stock_:" + orderPo.RequestId; // requestIdKey
            string orderSn = OrderUtil.GetOrderCode();// 订单号
                                                      //string orderSn = distributedOrderSn.CreateDistributedOrderSn(); // 分布式订单号

            // 2、执行秒杀
            var SeckillResult = RedisHelper.EvalSHA(memoryCache.Get<string>("luaSha"), ProductKey, UserBuyLimitKey, SeckillLimitKey, productCount, requestCountLimits, seckillLimitKeyExpire, requestIdKey, orderSn);
            if (!SeckillResult.ToString().Equals("1"))
            {
                throw new BizException(SeckillResult.ToString());
            }

            try
            {
                // throw new Exception("222");
                // 3、发送订单消息到rabbitmq 发送失败，消息回滚
                SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);
            }
            catch (Exception)
            {
                // 3.1 秒杀回滚
                RedisHelper.EvalSHA(memoryCache.Get<string>("luaShaCallback"), ProductKey, UserBuyLimitKey, productCount, requestIdKey, orderSn);

                // 3.2 抢购失败
                throw new BizException("抢购失败");

                // 3.3 少卖问题是允许的，100个商品 99个 100 个 100票
            }

            // 4、创建支付信息
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.OrderSn = orderSn;
            paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
            paymentDto.UserId = sysUser.UserId;
            paymentDto.ProductId = orderPo.ProductId;
            paymentDto.ProductName = orderPo.ProductName;

            return paymentDto;
        }*/

        /// <summary>
        /// 4.6、创建订单(redis + 消息队列 + lua + 方法幂等 + 失败回滚 + 分布式订单号)
        /// </summary>
        /// <param name="orderDto"></param>
        //[HttpPost]
        //public PaymentDto CreateOrder(SysUser sysUser, [FromForm] OrderPo orderPo)
        //{
        //    // 1、秒杀参数准备
        //    string ProductKey = Convert.ToString(orderPo.ProductId);// 商品key
        //    string SeckillLimitKey = "seckill_stock_:SeckillLimit" + orderPo.ProductCount; // 单品限流key
        //    string UserBuyLimitKey = "seckill_stock_:UserId" + sysUser.UserId + "ProductId" + orderPo.ProductId;// 用户购买限制key
        //    int productCount = orderPo.ProductCount; // 购买商品数量
        //    int requestCountLimits = 60000; // 单品限流数量
        //    int seckillLimitKeyExpire = 60;// 单品限流时间：单位秒
        //    string requestIdKey = "seckill_stock_:" + orderPo.RequestId; // requestIdKey
        //    string orderSn = distributedOrderSn.CreateDistributedOrderSn(); // 分布式订单号 "97006545732243456"

        //    // 2、执行秒杀
        //    var SeckillResult = RedisHelper.EvalSHA(memoryCache.Get<string>("luaSha"), ProductKey, UserBuyLimitKey, SeckillLimitKey, productCount, requestCountLimits, seckillLimitKeyExpire, requestIdKey, orderSn);
        //    if (!SeckillResult.ToString().Equals("1"))
        //    {
        //        throw new BizException(SeckillResult.ToString());
        //    }

        //    try
        //    {
        //        // 3、发送订单消息到rabbitmq
        //        SendOrderCreateMessage(sysUser.UserId, orderSn, orderPo);
        //    }
        //    catch (Exception)
        //    {
        //        // 3.1 秒杀回滚
        //        RedisHelper.EvalSHA(memoryCache.Get<string>("luaShaCallback"), ProductKey, UserBuyLimitKey, productCount, requestIdKey, orderSn);

        //        // 3.2 抢购失败
        //        throw new BizException("抢购失败");
        //    }

        //    // 4、创建支付信息
        //    PaymentDto paymentDto = new PaymentDto();
        //    paymentDto.OrderSn = orderSn;
        //    paymentDto.OrderTotalPrice = orderPo.OrderTotalPrice;
        //    paymentDto.UserId = sysUser.UserId;
        //    paymentDto.ProductId = orderPo.ProductId;
        //    paymentDto.ProductName = orderPo.ProductName;

        //    return paymentDto;
        //}

        /// <summary>
        /// 发送创建订单消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderSn"></param>
        /// <param name="orderPo"></param>
        private void SendOrderCreateMessage(int userId,string orderSn,OrderPo orderPo)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderPo, Order>();
            });

            IMapper mapper = configuration.CreateMapper();

            // 2、设置订单
            Order order = mapper.Map<OrderPo, Order>(orderPo);
            order.OrderSn = orderSn;
            order.OrderType = "1";// 订单类型(1、为秒杀订单)
            order.UserId = userId;

            // 3、设置订单项
            OrderItem orderItem = new OrderItem();
            orderItem.ItemCount = orderPo.ProductCount;
            orderItem.ItemPrice = orderPo.OrderTotalPrice;
            orderItem.ItemTotalPrice = orderPo.OrderTotalPrice;
            orderItem.ProductUrl = orderPo.ProductUrl;
            orderItem.ProductId = orderPo.ProductId;
            orderItem.OrderSn = orderSn;

            List<OrderItem> orderItems = new List<OrderItem>();
            orderItems.Add(orderItem);
            order.OrderItems = orderItems;

            //4.发送订单消息
            capPublisher.Publish("seckill.order", order);
        }
    }
}
