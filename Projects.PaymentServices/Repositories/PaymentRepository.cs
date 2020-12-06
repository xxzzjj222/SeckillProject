using Projects.PaymentServices.Context;
using Projects.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        public PaymentContext PaymentContext;
        public PaymentRepository(PaymentContext PaymentContext)
        {
            this.PaymentContext = PaymentContext;
        }
        public void Create(Payment Payment)
        {
            PaymentContext.Payments.Add(Payment);
            PaymentContext.SaveChanges();
        }

        public void Delete(Payment Payment)
        {
            PaymentContext.Payments.Remove(Payment);
            PaymentContext.SaveChanges();
        }

        public Payment GetPaymentById(int id)
        {
            return PaymentContext.Payments.Find(id);
        }

        public IEnumerable<Payment> GetPayments()
        {
            return PaymentContext.Payments.ToList();
        }

        public void Update(Payment Payment)
        {
            PaymentContext.Payments.Update(Payment);
            PaymentContext.SaveChanges();
        }
        public bool PaymentExists(int id)
        {
            return PaymentContext.Payments.Any(e => e.Id == id);
        }
    }
}
