using Projects.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPayments();
        Payment GetPaymentById(int id);
        void Create(Payment Payment);
        void Update(Payment Payment);
        void Delete(Payment Payment);
        bool PaymentExists(int id);
    }
}
