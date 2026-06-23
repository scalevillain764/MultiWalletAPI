using _user;
using _wallet;
namespace _transfer
{
    public class Transfer
    {
        public enum TransferStatus { Pending = 1, Completed = 2, Failed = 3}

        public Ulid Id { get; set; }
        public Ulid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null;

        public Ulid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null;

        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public TransferStatus Status { get; set; }
        public Transfer(Ulid fromWalletId, Ulid toWalletId, decimal amount)
        {
            Id = Ulid.NewUlid();
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
            Status = TransferStatus.Pending;
        }
    }
}