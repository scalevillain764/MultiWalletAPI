using _user;
using _wallet;
namespace _transfer
{
    public class Transfer
    {
        public Ulid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null;

        public Ulid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null;

        public decimal Amount { get; set; }
        public Transfer(Ulid fromWalletId, Ulid toWalletId, decimal amount)
        {
            ToWalletId = toWalletId;
            Amount = amount;
        }
    }
}