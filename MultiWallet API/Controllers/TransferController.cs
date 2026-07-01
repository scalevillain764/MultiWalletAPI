using _base_controller;
using _interfaces;
using _transfer_creation_dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace _transfer_controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransferController : BaseController
    {
        private readonly ITransferService _service;
        public TransferController(ITransferService service) => _service = service;

        [HttpPost]      
        public async Task<IActionResult> MakeTransferAsync([FromBody] TransferCreationDTO transferCreationDTO)
        {
            var userId = GetUserId();
            var rez = await _service.MakeTransferAsync(userId, transferCreationDTO);
            return ProcessResult(rez);
        }
    }
}
