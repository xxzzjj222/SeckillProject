using Projects.OrderServices.Models;
using Projects.OrderServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Services
{
    public class OrderServiceImpl: IOrderService
    {
        public readonly IOrderRepository OrderRepository;

        public OrderServiceImpl(IOrderRepository OrderRepository)
        {
            this.OrderRepository = OrderRepository;
        }

        public void Create(Order Order)
        {
            OrderRepository.Create(Order);
        }

        public void Delete(Order Order)
        {
            OrderRepository.Delete(Order);
        }

        public Order GetOrderById(int id)
        {
            return OrderRepository.GetOrderById(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return OrderRepository.GetOrders();
        }

        public void Update(Order Order)
        {
            OrderRepository.Update(Order);
        }

        public bool OrderExists(int id)
        {
            return OrderRepository.OrderExists(id);
        }
    }
}
