namespace _payment_creation_dto
{
    public class PaymentCreationDTO
    {
        public string WalletId { get; set; }
        public int Currency { get; set; }
        public decimal Amount { get; set; }
        public PaymentCreationDTO(string walletId, int currency, decimal amount)
        {
            WalletId = walletId;
            Currency = currency;
            Amount = amount;
        }
    }
}