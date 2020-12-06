using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.OrderServices.Models;
using Projects.OrderServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projects.OrderServices.Controllers
{
    /// <summary>
    /// 订单服务控制器
    /// </summary>
    [Route("Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        private readonly IOrderItemService orderItemService;

        public OrderController(IOrderService OrderService,
                            IOrderItemService orderItemService)
        {
            this.OrderService = OrderService;
            this.orderItemService = orderItemService;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return OrderService.GetOrders().ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var Order = OrderService.GetOrderById(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order Order)
        {
            if (id != Order.Id)
            {
                return BadRequest();
            }

            try
            {
                OrderService.Update(Order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Order> PostOrder(Order Order)
        {
            // 1、创建订单
            Order.Createtime = new DateTime();
            OrderService.Create(Order);

            // 2、创建订单项
            /*foreach (var orderItem in Order.OrderItems)
            {
                orderItemService.Create(orderItem);
            }*/
            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe("seckill.order")]
        public ActionResult<Order> CapPostOrder(Order Order)
        {
            // 1、创建订单
            Order.Createtime = new DateTime();
            OrderService.Create(Order);

            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(int id)
        {
            var Order = OrderService.GetOrderById(id);
            if (Order == null)
            {
                return NotFound();
            }

            OrderService.Delete(Order);
            return Order;
        }

        private bool OrderExists(int id)
        {
            return OrderService.OrderExists(id);
        }
    }
}
