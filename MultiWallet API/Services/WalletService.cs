using _context;
using _interfaces;
using _result;
using _tranfser_response_dto;
using _wallet;
using _wallet_creation_dto;
using _wallet_response_dto;
using Microsoft.EntityFrameworkCore;

namespace _wallet_service
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _context;
        public WalletService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<WalletResponseDTO>> AddWalletAsync(Ulid UserId, WalletCreationDTO walletCreationDTO)
        {
            bool currencyExists = await _context.Wallets
                .Where(x => x.UserId == UserId)
                .AnyAsync(x => (int)x._Currency == walletCreationDTO.CurrencyEnum);

            if (currencyExists)
                return Result<WalletResponseDTO>.Error("Счет с такой валютой уже существует", Result<WalletResponseDTO>.ErrorType.Validation);

            var newWallet = new Wallet(walletCreationDTO.Name, UserId, (Wallet.Currency)walletCreationDTO.CurrencyEnum);

            _context.Wallets.Add(newWallet);

            await _context.SaveChangesAsync();
            return Result<WalletResponseDTO>.Success(new WalletResponseDTO(newWallet));
        }
        public async Task<Result<WalletResponseDTO>> RemoveWalletAsync(Ulid UserId, Ulid WalletId)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<WalletResponseDTO>.Error("Счет не найден", Result<WalletResponseDTO>.ErrorType.NotFound);

            if (wallet.Balance > 0)
                return Result<WalletResponseDTO>.Error("Сумма на счете должна быть нулевая, чтобы его удалить", Result<WalletResponseDTO>.ErrorType.Forbidden);

            var responseDTO = new WalletResponseDTO(wallet);

            wallet.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Result<WalletResponseDTO>.Success(responseDTO);
        }
        public async Task<Result<WalletResponseDTO>> ChangeWalletNameAsync(Ulid UserId, Ulid WalletId, string NewName)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<WalletResponseDTO>.Error("Счет не найден", Result<WalletResponseDTO>.ErrorType.NotFound);

            wallet.Name = NewName;

            await _context.SaveChangesAsync();
            return Result<WalletResponseDTO>.Success(new WalletResponseDTO(wallet));
        }
        public async Task<Result<WalletResponseDTO>> ReplenishBalanceAsync(Ulid UserId, Ulid WalletId, decimal Amount) // пока так для примера, потом будет интеграция с ПС
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<WalletResponseDTO>.Error("Счет не найден", Result<WalletResponseDTO>.ErrorType.NotFound);

            wallet.Balance += Amount;

            await _context.SaveChangesAsync();
            return Result<WalletResponseDTO>.Success(new WalletResponseDTO(wallet));
        }
        public async Task<Result<List<WalletResponseDTO>>> GetAllAsync(Ulid UserId)
        {
            var rez = await _context.Wallets      
                .Where(x => x.UserId == UserId)
                .Select(x => new WalletResponseDTO(x))
                .ToListAsync();

            return Result<List<WalletResponseDTO>>.Success(rez);
        }
    }
}