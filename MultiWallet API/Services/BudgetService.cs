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

        public async Task<Result<BudgetResponseDTO>> MakeExpenseAsync(Ulid UserId, BudgetCreationDTO CreationDTO)
        {
            if (!Ulid.TryParse(CreationDTO.WalletId, out var WalletId))
                return Result<BudgetResponseDTO>.Error("Неверный номер счета", Result<BudgetResponseDTO>.ErrorType.Validation);

            using var transaction = await _context.Database.BeginTransactionAsync();

            var wallet = await _context.Wallets
                   .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<BudgetResponseDTO>.Error("Счет не найден", Result<BudgetResponseDTO>.ErrorType.NotFound);

            if (wallet.UserId != UserId)
                return Result<BudgetResponseDTO>.Error("Счет не принадлежит пользователю", Result<BudgetResponseDTO>.ErrorType.NotFound);

            if (CreationDTO.Amount > wallet.Balance)
                return Result<BudgetResponseDTO>.Error("Недостаточно средств", Result<BudgetResponseDTO>.ErrorType.Validation);

            wallet.Balance -= CreationDTO.Amount;

            var _transaction = new Transaction(UserId, WalletId,
                CreationDTO.Amount, Transaction.TransactionType.Expense,
                CreationDTO.Description, (Category)CreationDTO.Category);

            _context.Transactions.Add(_transaction);

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();

            return Result<BudgetResponseDTO>.Success(new BudgetResponseDTO(
                CreationDTO.WalletId,
                ((Category)CreationDTO.Category).ToString(),
                CreationDTO.Amount
                ));
        }

        public async Task<Result<BudgetResponseDTO>> MakeIncomeAsync(Ulid UserId, BudgetCreationDTO CreationDTO)
        {
            if (!Ulid.TryParse(CreationDTO.WalletId, out var WalletId))
                return Result<BudgetResponseDTO>.Error("Неверный номер счета", Result<BudgetResponseDTO>.ErrorType.Validation);

            using var transaction = await _context.Database.BeginTransactionAsync();

            var wallet = await _context.Wallets
                   .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<BudgetResponseDTO>.Error("Счет не найден", Result<BudgetResponseDTO>.ErrorType.NotFound);

            if (wallet.UserId != UserId)
                return Result<BudgetResponseDTO>.Error("Счет не принадлежит пользователю", Result<BudgetResponseDTO>.ErrorType.NotFound);

            wallet.Balance += CreationDTO.Amount;

            var _transaction = new Transaction(UserId, WalletId,
                 CreationDTO.Amount, Transaction.TransactionType.Income,
                 CreationDTO.Description, (Category)CreationDTO.Category);

            _context.Transactions.Add(_transaction);

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();

            return Result<BudgetResponseDTO>.Success(new BudgetResponseDTO(
                CreationDTO.WalletId,
                ((Category)CreationDTO.Category).ToString(),
                CreationDTO.Amount
                ));
        }
    }
}