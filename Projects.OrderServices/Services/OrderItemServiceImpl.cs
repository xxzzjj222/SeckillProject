using Projects.OrderServices.Models;
using Projects.OrderServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Services
{
    public class OrderItemServiceImpl: IOrderItemService
    {
        public readonly IOrderItemRepository OrderItemRepository;

        public OrderItemServiceImpl(IOrderItemRepository OrderItemRepository)
        {
            this.OrderItemRepository = OrderItemRepository;
        }

        public void Create(OrderItem OrderItem)
        {
            OrderItemRepository.Create(OrderItem);
        }

        public void Delete(OrderItem OrderItem)
        {
            OrderItemRepository.Delete(OrderItem);
        }

        public OrderItem GetOrderItemById(int id)
        {
            return OrderItemRepository.GetOrderItemById(id);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return OrderItemRepository.GetOrderItems();
        }

        public void Update(OrderItem OrderItem)
        {
            OrderItemRepository.Update(OrderItem);
        }

        public bool OrderItemExists(int id)
        {
            return OrderItemRepository.OrderItemExists(id);
        }
    }
}
