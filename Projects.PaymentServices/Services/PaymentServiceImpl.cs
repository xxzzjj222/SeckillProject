using Projects.PaymentServices.Models;
using Projects.PaymentServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Services
{
    public class PaymentServiceImpl:IPaymentService
    {
        public readonly IPaymentRepository PaymentRepository;

        public PaymentServiceImpl(IPaymentRepository PaymentRepository)
        {
            this.PaymentRepository = PaymentRepository;
        }

        public void Create(Payment Payment)
        {
            PaymentRepository.Create(Payment);
        }

        public void Delete(Payment Payment)
        {
            PaymentRepository.Delete(Payment);
        }

        public Payment GetPaymentById(int id)
        {
            return PaymentRepository.GetPaymentById(id);
        }

        public IEnumerable<Payment> GetPayments()
        {
            return PaymentRepository.GetPayments();
        }

        public void Update(Payment Payment)
        {
            PaymentRepository.Update(Payment);
        }

        public bool PaymentExists(int id)
        {
            return PaymentRepository.PaymentExists(id);
        }
    }
}
