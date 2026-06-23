using _wallet;
using _interfaces;
using _context;
using _result;

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
                return Result<WalletResponseDTO>.Error("Счет с такой валютой уже существует");

            var newWallet = new Wallet(walletCreationDTO.Name, UserId, (Wallet.Currency)walletCreationDTO.CurrencyEnum);
        }
    }
}