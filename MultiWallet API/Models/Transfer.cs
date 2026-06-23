using _user;
using _wallet;
namespace _transfer
{
    public class Transfer
    {
        public Ulid FromUserId { get; set; }
        public User FromUser { get; set; } = null;

        public Ulid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null;

        public Ulid ToUserId { get; set; }
        public User ToUser { get; set; } = null;

        public Ulid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null;

        public decimal Amount { get; set; }
        public Transfer(Ulid fromUserId, Ulid fromWalletId, Ulid toUserId, Ulid toWalletId, decimal amount)
        {
            FromUserId = fromUserId;
            FromWalletId = fromWalletId;
            ToUserId = toUserId;
            ToWalletId = toWalletId;
            Amount = amount;
        }
    }
}