﻿using Projects.OrderServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.OrderServices.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        void Create(Order Order);
        void Update(Order Order);
        void Delete(Order Order);
        bool OrderExists(int id);
    }
}
