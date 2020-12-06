using Projects.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Repositories
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetOrderItems();
        OrderItem GetOrderItemById(int id);
        void Create(OrderItem OrderItem);
        void Update(OrderItem OrderItem);
        void Delete(OrderItem OrderItem);
        bool OrderItemExists(int id);
    }
}
