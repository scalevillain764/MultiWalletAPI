using _tranfser_response_dto;
using _transfer_creation_dto;
using _interfaces;
using _context;
using _result;
using _transaction;
using Microsoft.EntityFrameworkCore;
using _transfer;
namespace _transfer_service
{
    public class TransferService : ITransferService
    {
        private readonly AppDbContext _context;
        public TransferService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<Result<TransferResponseDTO>> MakeTransfer(Ulid UserId, TransferCreationDTO transferCreationDTO)
        {
            if (!Ulid.TryParse(transferCreationDTO.FromWalletId, out var fromWalletId))
                return Result<TransferResponseDTO>.Error("Неверный № счета отправителя");
               
            if (!Ulid.TryParse(transferCreationDTO.ToWalletId, out var toWalletId))
                return Result<TransferResponseDTO>.Error("Неверный № счета получателя");         

            if (toWalletId == fromWalletId)
                return Result<TransferResponseDTO>.Error("Нельзя перевести деньги на тот же счёт");
           
            using var transaction = await _context.Database.BeginTransactionAsync();
         
            try
            {
                var toWallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.Id == toWalletId);

                if (toWallet == null)
                    return Result<TransferResponseDTO>.Error("Счет получателя не найден");

                var fromWallet = await _context.Wallets
                   .FirstOrDefaultAsync(x => x.Id == fromWalletId);

                if (fromWallet == null)
                    return Result<TransferResponseDTO>.Error("Счет отправителя не найден");

                if (transferCreationDTO.Amount > fromWallet.Balance)
                    return Result<TransferResponseDTO>.Error("Недостаточно средств");

                fromWallet.Balance -= transferCreationDTO.Amount;
                toWallet.Balance += transferCreationDTO.Amount;

                var fromWalletTransaction = new Transaction(UserId, fromWalletId, transferCreationDTO.Amount, Transaction.TransactionType.Transfer, transferCreationDTO.Description);
                var toWalletTransaction = new Transaction(toWallet.UserId, toWalletId, transferCreationDTO.Amount, Transaction.TransactionType.Transfer, transferCreationDTO.Description);
                var newTransfer = new Transfer(fromWalletId, toWalletId, transferCreationDTO.Amount);

                _context.Transactions.Add(fromWalletTransaction);
                _context.Transactions.Add(toWalletTransaction);
                _context.Transfers.Add(newTransfer);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Result<TransferResponseDTO>.Success(new TransferResponseDTO(newTransfer));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<TransferResponseDTO>.Error("Что-то пошло не так");
            }

            // потом доделать курсы валют с апи!
        }
    }
}