using Projects.OrderServices.Context;
using Projects.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public OrderContext OrderContext;
        public OrderItemRepository(OrderContext OrderContext)
        {
            this.OrderContext = OrderContext;
        }
        public void Create(OrderItem OrderItem)
        {
            OrderContext.OrderItems.Add(OrderItem);
            OrderContext.SaveChanges();
        }

        public void Delete(OrderItem OrderItem)
        {
            OrderContext.OrderItems.Remove(OrderItem);
            OrderContext.SaveChanges();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return OrderContext.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return OrderContext.OrderItems.ToList();
        }

        public void Update(OrderItem OrderItem)
        {
            OrderContext.OrderItems.Update(OrderItem);
            OrderContext.SaveChanges();
        }
        public bool OrderItemExists(int id)
        {
            return OrderContext.OrderItems.Any(e => e.Id == id);
        }
    }
}
