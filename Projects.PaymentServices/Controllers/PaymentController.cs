using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.PaymentServices.Models;
using Projects.PaymentServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.PaymentServices.Controllers
{
    [Route("Payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService PaymentService;

        public PaymentController(IPaymentService PaymentService)
        {
            this.PaymentService = PaymentService;
        }

        // GET: api/Payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return PaymentService.GetPayments().ToList();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var Payment = PaymentService.GetPaymentById(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutPayment(int id, Payment Payment)
        {
            if (id != Payment.Id)
            {
                return BadRequest();
            }

            try
            {
                PaymentService.Update(Payment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Payment> PostPayment(Payment Payment)
        {
            PaymentService.Create(Payment);
            return CreatedAtAction("GetPayment", new { id = Payment.Id }, Payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public ActionResult<Payment> DeletePayment(int id)
        {
            var Payment = PaymentService.GetPaymentById(id);
            if (Payment == null)
            {
                return NotFound();
            }

            PaymentService.Delete(Payment);
            return Payment;
        }

        private bool PaymentExists(int id)
        {
            return PaymentService.PaymentExists(id);
        }
    }
}
