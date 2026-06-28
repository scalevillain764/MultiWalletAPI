using _base_controller;
using _interfaces;
using _wallet_creation_dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace _interfaces
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : BaseController
    {
        private readonly IWalletService _service;
        public WalletController(IWalletService service)
        {
            _service = service;
        }

        [HttpPut]
        [Route("create-wallet")]
        public async Task<IActionResult> AddWalletAsync([FromBody] WalletCreationDTO DTO)
        {
            var userId = GetUserId();
            var rez = await _service.AddWalletAsync(userId, DTO);
            return ProcessResult(rez);
        }

        [HttpDelete]
        [Route("remove-wallet")]
        public async Task<IActionResult> RemoveWalletAsync([FromQuery] string walletIdStr)
        {
            var walletId = ConverStringToUlid(walletIdStr);
            var userId = GetUserId();
            var rez = await _service.RemoveWalletAsync(userId, walletId);
            return ProcessResult(rez);
        }

        [HttpPut]
        [Route("change-wallet-name")]
        public async Task<IActionResult> ChangeWalletNameAsync([FromQuery] string walletIdStr, [FromQuery] string newName)
        {
            var walletId = ConverStringToUlid(walletIdStr);
            var userId = GetUserId();
            var rez = await _service.ChangeWalletNameAsync(userId, walletId, newName);
            return ProcessResult(rez);
        }

        [HttpPut]
        [Route("replenish-wallet-balance")]
        public async Task<IActionResult> ReplenishBalanceAsync([FromQuery] string walletIdStr, [FromQuery] decimal amount)
        {
            var walletId = ConverStringToUlid(walletIdStr);
            var userId = GetUserId();
            var rez = await _service.ReplenishBalanceAsync(userId, walletId, amount);
            return ProcessResult(rez);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllWalletsByUserAsync()
        {
            var userId = GetUserId();
            var rez = await _service.GetAllAsync(userId);
            return ProcessResult(rez);
        }
    }
}
