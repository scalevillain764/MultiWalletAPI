using _interfaces;
using _context;
using _result;
using _transaction;
using _transfer;

using Microsoft.EntityFrameworkCore;

using _tranfser_response_dto;
using _transfer_creation_dto;
using _exchange_response;

namespace _transfer_service
{
    public class TransferService : ITransferService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TransferService> _logger;
        public TransferService(AppDbContext context, IHttpClientFactory factory, IConfiguration configuration, ILogger<TransferService> logger)
        {
            _configuration = configuration;
            _factory = factory;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<TransferResponseDTO>> MakeTransferAsync(Ulid UserId, TransferCreationDTO transferCreationDTO)
        {
            if (!Ulid.TryParse(transferCreationDTO.FromWalletId, out var fromWalletId))
                return Result<TransferResponseDTO>.Error("Неверный № счета отправителя", Result<TransferResponseDTO>.ErrorType.Validation);
               
            if (!Ulid.TryParse(transferCreationDTO.ToWalletId, out var toWalletId))
                return Result<TransferResponseDTO>.Error("Неверный № счета получателя", Result<TransferResponseDTO>.ErrorType.Validation);         

            if (toWalletId == fromWalletId)
                return Result<TransferResponseDTO>.Error("Нельзя перевести деньги на тот же счёт", Result<TransferResponseDTO>.ErrorType.Forbidden);
  
            string api_key = _configuration["Exchange_api_key"];
          
            using var transaction = await _context.Database.BeginTransactionAsync();

            var toWallet = await _context.Wallets
               .FirstOrDefaultAsync(x => x.Id == toWalletId);

            if (toWallet == null)
                return Result<TransferResponseDTO>.Error("Счет получателя не найден", Result<TransferResponseDTO>.ErrorType.NotFound);

            var fromWallet = await _context.Wallets
               .FirstOrDefaultAsync(x => x.Id == fromWalletId);

            if (fromWallet == null)
                return Result<TransferResponseDTO>.Error("Счет отправителя не найден", Result<TransferResponseDTO>.ErrorType.NotFound);

            if (fromWallet.UserId != UserId)
                return Result<TransferResponseDTO>.Error("Счет не принадлежит пользователю", Result<TransferResponseDTO>.ErrorType.Forbidden);

            if (transferCreationDTO.Amount > fromWallet.Balance)
                return Result<TransferResponseDTO>.Error("Недостаточно средств", Result<TransferResponseDTO>.ErrorType.Validation);

            // http client
            var client = _factory.CreateClient();
            ExchangeApiResponse ExchangeResponse = new();

            try
            {
                var response = await client.GetAsync($"https://v6.exchangerate-api.com/v6/{api_key}/latest/USD");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        ExchangeResponse = await response.Content.ReadFromJsonAsync<ExchangeApiResponse>();
                    }
                    catch (OperationCanceledException oce)
                    {
                        _logger.LogError($"Parse error: {oce.Message}");
                    }
                }

            }
            catch (HttpRequestException ex) // потом поменять
            {
                _logger.LogError($"Network error during creating request: {ex.Message}");
            }

            if (ExchangeResponse.ConversionRates.Count == 0)
                return Result<TransferResponseDTO>.Error("Ошибка валюты", Result<TransferResponseDTO>.ErrorType.Conflict);

            decimal fromWalletExchange = ExchangeResponse.ConversionRates[$"{fromWallet._Currency.ToString()}"];
            decimal toWalletExchange = ExchangeResponse.ConversionRates[$"{toWallet._Currency.ToString()}"];
            decimal exchangeRate = Math.Round(fromWalletExchange / toWalletExchange, 2);

            decimal convertedAmount = Math.Round(transferCreationDTO.Amount / fromWalletExchange * toWalletExchange, 2);

            fromWallet.Balance -= transferCreationDTO.Amount;
            toWallet.Balance += convertedAmount;

            var fromWalletTransaction = new Transaction(UserId, fromWalletId, transferCreationDTO.Amount, Transaction.TransactionType.Transfer, transferCreationDTO.Description, null);
            fromWalletTransaction.Status = Transaction.TransactionStatus.Completed;

            var toWalletTransaction = new Transaction(toWallet.UserId, toWalletId, convertedAmount, Transaction.TransactionType.Transfer, transferCreationDTO.Description, null);
            toWalletTransaction.Status = Transaction.TransactionStatus.Completed;

            var newTransfer = new Transfer(fromWallet.UserId, fromWalletId, toWalletId,
                transferCreationDTO.Amount, convertedAmount, exchangeRate,
                fromWallet._Currency, toWallet._Currency);

            _context.Transactions.Add(fromWalletTransaction);
            _context.Transactions.Add(toWalletTransaction);
            _context.Transfers.Add(newTransfer);

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();

            _logger.LogInformation($"Transfer {newTransfer.Id.ToString()} created succesfully");
            return Result<TransferResponseDTO>.Success(new TransferResponseDTO(newTransfer));
        }
    }
}