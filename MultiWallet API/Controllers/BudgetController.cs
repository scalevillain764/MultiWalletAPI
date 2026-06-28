using _base_controller;
using _budget_creation_dto;
using _interfaces;
using _transfer_creation_dto;
using Microsoft.AspNetCore.Mvc;
namespace _budget_controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : BaseController
    {
        private readonly IBudgetService _service;
        public BudgetController(IBudgetService service)
        {
            _service = service;
        }

        [HttpPut]
        [Route("create-expense")]
        public async Task<IActionResult> MakeExpenseAsync([FromBody] BudgetCreationDTO CreationDTO)
        {
            var userId = GetUserId();
            var rez = await _service.MakeExpenseAsync(userId, CreationDTO);
            return ProcessResult(rez);
        }

        [HttpPut]
        [Route("create-income")]
        public async Task<IActionResult> MakeIncomeAsync([FromBody] BudgetCreationDTO CreationDTO)
        {
            var userId = GetUserId();
            var rez = await _service.MakeIncomeAsync(userId, CreationDTO);
            return ProcessResult(rez);
        }
    }
}