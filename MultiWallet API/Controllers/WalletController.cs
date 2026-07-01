using _base_controller;
using _interfaces;
using _wallet_creation_dto;
using Microsoft.AspNetCore.Mvc;
using _patch_wallet_dto;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace _interfaces
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : BaseController
    {
        private readonly IWalletService _service;
        public WalletController(IWalletService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddWalletAsync([FromBody] WalletCreationDTO DTO)
        {
            var userId = GetUserId();
            var rez = await _service.AddWalletAsync(userId, DTO);
            return ProcessResult(rez);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveWalletAsync([FromQuery] string walletIdStr)
        {
            var walletId = ConvertStringToUlid(walletIdStr);
            var userId = GetUserId();
            var rez = await _service.RemoveWalletAsync(userId, walletId);
            return ProcessResult(rez);
        }

        [HttpPut]
        [Route("update-name")]
        public async Task<IActionResult> ChangeWalletNameAsync([FromQuery] string walletIdStr, [FromBody] WalletRenameDTO DTO)
        {
            var walletId = ConvertStringToUlid(walletIdStr);
            var userId = GetUserId();
            var rez = await _service.ChangeWalletNameAsync(userId, walletId, DTO.Name);
            return ProcessResult(rez);
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllWalletsByUserAsync()
        {
            var userId = GetUserId();
            var rez = await _service.GetAllAsync(userId);
            return ProcessResult(rez);
        }
    }
}
