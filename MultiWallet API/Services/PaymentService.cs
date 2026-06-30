using _category;
using _context;
using _interfaces;
using _payment_creation_dto;
using _result;
using _transaction;
using _user;
using _wallet;
using Microsoft.EntityFrameworkCore;
using Yandex;
using Yandex.Checkout.V3;
namespace _payment_service
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public PaymentService(AppDbContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Result<string>> MakePayment(Ulid UserId, PaymentCreationDTO DTO)
        {
            if (!Ulid.TryParse(DTO.WalletId, out var WalletId))
                return Result<string>.Error("Неверный номер счета", Result<string>.ErrorType.Validation);

            var wallet = await _context.Wallets
                   .Where(x => (int)x._Currency == DTO.Currency)
                   .FirstOrDefaultAsync(x => x.Id == WalletId);

            if (wallet == null)
                return Result<string>.Error("Счет не найден", Result<string>.ErrorType.NotFound);

            if (wallet.UserId != UserId)
                return Result<string>.Error("Счет не принадлежит пользователю", Result<string>.ErrorType.NotFound);

            var newTransaction = new Transaction(UserId, WalletId, DTO.Amount, Transaction.TransactionType.Deposit, null, Category.Other);

            string secretKey = _configuration["Ukassa_api_key"];
            string shopId = _configuration["ShopId"];

            var newClient = new Client(shopId, secretKey); // создаем клиента
            AsyncClient asyncClient = newClient.MakeAsync(); // делаем клиента асинхронным

            var newPayment = new Payment
            {
                Amount = new Amount { Value = DTO.Amount, Currency =  wallet._Currency.ToString() },
                Capture = true,
                Confirmation = new Confirmation { Type = ConfirmationType.Redirect, ReturnUrl = _configuration["ReturnURL"]},
                Description = "Тестовый платеж в C#"
            };

            try
            {   // уникальный ключ идемпотентности
                string idempotenceKey = Guid.NewGuid().ToString();

                // отправка запроса
                Payment payment = await asyncClient.CreatePaymentAsync(newPayment, idempotenceKey);

               /*Console.WriteLine($"Платеж создан. ID: {payment.Id}");
                Console.WriteLine($"Статус платежа: {payment.Status}");*/

                string paymentUrl = payment.Confirmation.ConfirmationUrl;
                return Result<string>.Success(paymentUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании платежа: {ex.Message}");
                return Result<string>.Error("Что-то пошло не так", Result<string>.ErrorType.Conflict); // ?
            }
        }
    }
}