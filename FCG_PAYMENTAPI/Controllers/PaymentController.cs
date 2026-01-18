using FCG_PAYMENTAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG_PAYMENTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpGet("GetPaymentStatus")]
        public IActionResult GetPaymentStatus(int idSales)
        {
            if (idSales == 0)
                return BadRequest("Preencha os dados da venda!");
            return Ok(_paymentService.GetPaymentStatus(idSales));
        }

        [Authorize]
        [HttpPost("ConfirmPayment")]
        public IActionResult ConfirmPayment(int idSales)
        {
            if (idSales == 0)
                return BadRequest("Preencha os dados da venda!");
            var retorno = _paymentService.ConfirmPayment(idSales);
            if (retorno.Success)
                return Ok();
            else return BadRequest(retorno.Error);
        }

        [Authorize]
        [HttpPost("CancelPayment")]
        public IActionResult CancelPayment(int idSales)
        {
            if (idSales == 0)
                return BadRequest("Preencha os dados da venda!");
            var retorno = _paymentService.CancelPayment(idSales);
            if (retorno.Success)
                return Ok();
            else return BadRequest(retorno.Error);
        }

        [Authorize]
        [HttpPost("RefundPayment")]
        public IActionResult RefundPayment(int idSales)
        {
            if (idSales == 0)
                return BadRequest("Preencha os dados da venda!");
            var retorno = _paymentService.RefundPayment(idSales);
            if (retorno.Success)
                return Ok();
            else return BadRequest(retorno.Error);
        }
    }
}
