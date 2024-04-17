using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_app_demo.Entidades;
using web_app_demo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_app_demo.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private IPayment _payment;
        private ILogger<PaymentsController> _logger;

        public PaymentsController(IPayment payment, ILogger<PaymentsController> logger)
        {
            this._payment = payment;
            this._logger = logger;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IResult Post([FromBody] RequestPayment value)
        {
            _logger.LogInformation($"Procesando pago {value.Payment.Id}");
            var result = _payment.CreatePaymentAsync(value).Result;

            _logger.LogInformation("Traza de la ejecución del procedimiento almacenado");
            _logger.LogInformation($"{TraceExtension.TraceFormat(result.Trace as string)}");

            return result.Match(
                onSuccess: () => Results.Ok(result.Data),
                onFailure: error => Results.BadRequest(error));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

