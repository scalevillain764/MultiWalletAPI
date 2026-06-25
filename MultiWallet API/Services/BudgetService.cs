using _interfaces;
using _result;
using _transaction;
using _category;
using _wallet;

using _budget_response_dto;
using _budget_creation_dto;
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

        public async Task<Result<BudgetResponseDTO>> MakeExpense(Ulid UserId, BudgetCreationDTO CreationDTO)
        {
            if (!Ulid.TryParse(CreationDTO.WalletId, out var WalletId))
                return Result<BudgetResponseDTO>.Error("Неверный номер счета");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var wallet = await _context.Wallets
                    .FirstOrDefaultAsync(x => x.Id == WalletId);

                if (wallet == null)
                    return Result<BudgetResponseDTO>.Error("Счет не найден");

                if (wallet.UserId != UserId)
                    return Result<BudgetResponseDTO>.Error("Счет не принадлежит пользователю");

                if(CreationDTO.Amount > wallet.Balance)
                    return Result<BudgetResponseDTO>.Error("Недостаточно средств");

                wallet.Balance -= CreationDTO.Amount;

                if (!Enum.TryParse<Category>(CreationDTO.Category, out var cat))
                    return Result<BudgetResponseDTO>.Error("Неверная категория");

                var _transaction = new Transaction(UserId, WalletId,
                    CreationDTO.Amount, Transaction.TransactionType.Expense,
                    CreationDTO.Description, cat);

                _context.Transactions.Add(_transaction);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Result<BudgetResponseDTO>.Success(new BudgetResponseDTO(
                    CreationDTO.WalletId,
                    CreationDTO.Category,
                    CreationDTO.Amount
                    ));
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return Result<BudgetResponseDTO>.Error("Что-то пошло не так");
            }
        }

        public async Task<Result<BudgetResponseDTO>> MakeIncome(Ulid UserId, BudgetCreationDTO CreationDTO)
        {
            if (!Ulid.TryParse(CreationDTO.WalletId, out var WalletId))
                return Result<BudgetResponseDTO>.Error("Неверный номер счета");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var wallet = await _context.Wallets
                    .FirstOrDefaultAsync(x => x.Id == WalletId);

                if (wallet == null)
                    return Result<BudgetResponseDTO>.Error("Счет не найден");

                if (wallet.UserId != UserId)
                    return Result<BudgetResponseDTO>.Error("Счет не принадлежит пользователю");

                wallet.Balance += CreationDTO.Amount;

                if (!Enum.TryParse<Category>(CreationDTO.Category, out var cat))
                    return Result<BudgetResponseDTO>.Error("Неверная категория");

                var _transaction = new Transaction(UserId, WalletId,
                    CreationDTO.Amount, Transaction.TransactionType.Income,
                    CreationDTO.Description, cat);

                _context.Transactions.Add(_transaction);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Result<BudgetResponseDTO>.Success(new BudgetResponseDTO(
                    CreationDTO.WalletId,
                    CreationDTO.Category,
                    CreationDTO.Amount
                    ));
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return Result<BudgetResponseDTO>.Error("Что-то пошло не так");
            }
        }
    }
}