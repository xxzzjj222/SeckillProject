using Microsoft.EntityFrameworkCore;
using Projects.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Context
{
    public class PaymentContext:DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options)
            :base(options)
        {

        }

        public DbSet<Payment> Payments { get; set; }
    }
}
