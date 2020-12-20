using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeckillAggregateServices.Models.PaymentService;
using SeckillAggregateServices.Pos.PaymentService;
using SeckillAggregateServices.Services.PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 支付控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentClient paymentClient;

        public PaymentController(IPaymentClient paymentClient)
        {
            this.paymentClient = paymentClient;
        }

        [HttpPost]
        public Payment Post([FromForm] PaymentPo paymentPo)
        {
            ////1.支付信息
            //var configuration = new MapperConfiguration(cfg =>
            //  {
            //      cfg.CreateMap<PaymentPo, Payment>();
            //  });

            //var mapper = configuration.CreateMapper();

            //var payment = mapper.Map<Payment>(paymentPo);

            //payment.UserId = 1;
            // 1、支付信息
            Payment payment = new Payment();
            payment.PaymentType = paymentPo.PaymentType;
            payment.OrderId = paymentPo.OrderId;
            payment.PaymentPrice = paymentPo.OrderTotalPrice;
            payment.OrderSn = paymentPo.OrderSn;
            payment.UserId = 1;

            //2.创建支付订单
            payment = paymentClient.Pay(payment);

            return payment;
        }
    }
}
