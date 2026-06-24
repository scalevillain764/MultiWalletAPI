using _interfaces;
using _result;
using _transaction;
using _category;

using _expense_response_dto;
using _expense_creation_dto;
using _context;
using Microsoft.EntityFrameworkCore;

namespace _budget_service
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _context;
        public BudgetService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<ExpenseResponseDTO>> MakeExpense(Ulid UserId, ExpenseCreationDTO expenseCreationDTO)
        {
            if (!Ulid.TryParse(expenseCreationDTO.WalletId, out var WalletId))
                return Result<ExpenseResponseDTO>.Error("Неверный номер счета");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var wallet = await _context.Wallets
                    .FirstOrDefaultAsync(x => x.Id == WalletId);

                if(wallet == null)
                    return Result<ExpenseResponseDTO>.Error("Счет не найден");

                if (wallet.UserId != UserId)
                    return Result<ExpenseResponseDTO>.Error("Счет не принадлежит пользователю");

                wallet.Balance -= expenseCreationDTO.Amount;

                if(!Enum.TryParse<Category>(expenseCreationDTO.Category, out var cat))
                    return Result<ExpenseResponseDTO>.Error("Неверная категория");

                var _transaction = new Transaction(UserId, WalletId,
                    expenseCreationDTO.Amount, Transaction.TransactionType.Expense,
                    expenseCreationDTO.Description, cat);

                _context.Transactions.Add(_transaction);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Result<ExpenseResponseDTO>.Success(new ExpenseResponseDTO(
                    expenseCreationDTO.WalletId,
                    expenseCreationDTO.Category,
                    expenseCreationDTO.Amount
                    ));
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return Result<ExpenseResponseDTO>.Error("Что-то пошло не так");
            }
        }
    }
}