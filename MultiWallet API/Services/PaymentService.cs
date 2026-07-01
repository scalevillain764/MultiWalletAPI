using _category;
using _context;
using _interfaces;
using _payment_creation_dto;
using _result;
using _transaction;
using _user;
using _wallet;
using _yoo_kassa_dto;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Yandex;
using Yandex.Checkout.V3;
namespace _payment_service
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(AppDbContext context, IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<Result<string>> MakePaymentAsync(Ulid UserId, PaymentCreationDTO DTO)
        {
            if (!Ulid.TryParse(DTO.WalletId, out var WalletId))
                return Result<string>.Error("Неверный номер счета", Result<string>.ErrorType.Validation);

            var wallet = await _context.Wallets              
                   .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<string>.Error("Счет не найден", Result<string>.ErrorType.NotFound);

            if ((int)wallet._Currency != DTO.Currency)
                return Result<string>.Error("Неверная валюта", Result<string>.ErrorType.Validation);

            if (wallet.UserId != UserId)
                return Result<string>.Error("Счет не принадлежит пользователю", Result<string>.ErrorType.NotFound);

            var newTransaction = new Transaction(UserId, WalletId, DTO.Amount, Transaction.TransactionType.Deposit, null, Category.Other);

            string secretKey = _configuration["Ukassa_api_key"];
            string shopId = _configuration["ShopId"];

            var newClient = new Client(shopId, secretKey); // создаем клиента
            AsyncClient asyncClient = newClient.MakeAsync(); // делаем клиента асинхронным

            var newPayment = new Payment
            {
                Amount = new Yandex.Checkout.V3.Amount { Value = DTO.Amount, Currency = wallet._Currency.ToString() },
                Capture = true,
                Confirmation = new Confirmation { Type = ConfirmationType.Redirect, ReturnUrl = _configuration["ReturnURL"] },
                Description = DTO.Description != null ? DTO.Description : "Описание отсутствует"
            };
           
            try
            {   // уникальный ключ идемпотентности
                string idempotenceKey = Guid.NewGuid().ToString();

                // отправка запроса
                Payment payment = await asyncClient.CreatePaymentAsync(newPayment, idempotenceKey);

                _context.Transactions.Add(newTransaction);

                string paymentUrl = payment.Confirmation.ConfirmationUrl;

                newTransaction.ProviderPaymentId = payment.Id;

                await _context.SaveChangesAsync();
                return Result<string>.Success(paymentUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Creating payment error: {ex.Message}");
                return Result<string>.Error("Что-то пошло не так", Result<string>.ErrorType.Conflict); // ?
            }
        }

        public async Task PaymentProcessAsync(YooKassaDTO DTO)
        {
            var _transaction = await _context.Transactions
                .Include(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.ProviderPaymentId == DTO.Object.PaymentId);

            if (_transaction == null)
                return;

            string secretKey = _configuration["Ukassa_api_key"];
            string shopId = _configuration["ShopId"];

            var newClient = new Client(shopId, secretKey); // создаем клиента
            AsyncClient asyncClient = newClient.MakeAsync(); // делаем клиента асинхронным

            var payment = await asyncClient.GetPaymentAsync(_transaction.ProviderPaymentId);

            if (payment == null)
                return;

            if (payment.Status != PaymentStatus.Succeeded)
                return;

            using var transaction = await _context.Database.BeginTransactionAsync();

            if (_transaction.Status == Transaction.TransactionStatus.Completed)
                return;

            _transaction.Wallet.Balance += _transaction.Amount;
            _transaction.Status = Transaction.TransactionStatus.Completed;

            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();

            _logger.LogInformation($"Payment {payment.Id} created successfully");
        }
    }
}