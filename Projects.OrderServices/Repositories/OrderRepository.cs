using Projects.OrderServices.Context;
using Projects.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderContext OrderContext;
        public OrderRepository(OrderContext OrderContext)
        {
            this.OrderContext = OrderContext;
        }
        public void Create(Order Order)
        {
            OrderContext.Orders.Add(Order);
            OrderContext.SaveChanges();
        }

        public void Delete(Order Order)
        {
            OrderContext.Orders.Remove(Order);
            OrderContext.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return OrderContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return OrderContext.Orders.ToList();
        }

        public void Update(Order Order)
        {
            OrderContext.Orders.Update(Order);
            OrderContext.SaveChanges();
        }
        public bool OrderExists(int id)
        {
            return OrderContext.Orders.Any(e => e.Id == id);
        }
    }
}
