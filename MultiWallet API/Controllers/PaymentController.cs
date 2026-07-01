using _base_controller;
using _interfaces;
using _payment_creation_dto;
using _yoo_kassa_dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace _payment_controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> MakePaymentAsync([FromBody] PaymentCreationDTO DTO)
        {
            var userId = GetUserId();
            var rez = await _service.MakePaymentAsync(userId, DTO);
            return ProcessResult(rez);
        }

        [HttpPost]
        [Route("webhook")]
        public async Task TaskProcessWebHookAsync([FromBody] YooKassaDTO DTO)
        {
            await _service.PaymentProcessAsync(DTO);
        }
    }
}