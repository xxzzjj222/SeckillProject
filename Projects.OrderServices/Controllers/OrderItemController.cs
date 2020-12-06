using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.OrderServices.Models;
using Projects.OrderServices.Services;
using System.Collections.Generic;
using System.Linq;

namespace Projects.OrderServices.Controllers
{
    /// <summary>
    /// 订单项服务控制器
    /// </summary>
    [Route("Order/{OrderId}/OrderItems")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService OrderItemService;

        public OrderItemController(IOrderItemService OrderItemService)
        {
            this.OrderItemService = OrderItemService;
        }

        // GET: api/OrderItems
        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> GetOrderItems()
        {
            return OrderItemService.GetOrderItems().ToList();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetOrderItem(int id)
        {
            var OrderItem = OrderItemService.GetOrderItemById(id);

            if (OrderItem == null)
            {
                return NotFound();
            }

            return OrderItem;
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutOrderItem(int id, OrderItem OrderItem)
        {
            if (id != OrderItem.Id)
            {
                return BadRequest();
            }

            try
            {
                OrderItemService.Update(OrderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<OrderItem> PostOrderItem(OrderItem OrderItem)
        {
            OrderItemService.Create(OrderItem);
            return CreatedAtAction("GetOrderItem", new { id = OrderItem.Id }, OrderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public ActionResult<OrderItem> DeleteOrderItem(int id)
        {
            var OrderItem = OrderItemService.GetOrderItemById(id);
            if (OrderItem == null)
            {
                return NotFound();
            }

            OrderItemService.Delete(OrderItem);
            return OrderItem;
        }

        private bool OrderItemExists(int id)
        {
            return OrderItemService.OrderItemExists(id);
        }
    }
}
