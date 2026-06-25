using _user;
using _wallet;
namespace _transfer
{
    public class Transfer
    {
        public Ulid SourceUserId { get; set; }
        public User User { get; set; } = null!;
        public Ulid Id { get; set; }

        public Ulid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null!;

        public Ulid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null!;

        public decimal SourceAmount { get; set; }
        public decimal DestinationAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        
        public Wallet.Currency SourceCurrency { get; set; }
        public Wallet.Currency DestinationCurrency { get; set; }

        public DateTime CreatedAt { get; set; }

        public Transfer(Ulid sourceUserId, Ulid fromWalletId, Ulid toWalletId,
            decimal sourceAmount, decimal destinationAmount, decimal exhcangeRate,
            Wallet.Currency sourceCurrency, Wallet.Currency destinationCurrency)
        {
            Id = Ulid.NewUlid();

            SourceUserId = sourceUserId;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;

            SourceAmount = sourceAmount;
            DestinationAmount = destinationAmount;
            ExchangeRate = exhcangeRate;

            SourceCurrency = sourceCurrency;
            DestinationCurrency = destinationCurrency;

            CreatedAt = DateTime.UtcNow;
        }
    }
}