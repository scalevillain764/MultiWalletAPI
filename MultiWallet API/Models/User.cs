using _interfaces;
using _wallet;
using _transaction;
using _transfer;
namespace _user
{
    public class User : IEntity
    {
        public Ulid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string? RefreshToken { get; set; }
        public List<Wallet> Wallets { get; set; } = new(); // np
        public List<Transaction> Transactions { get; set; } = new();
        public List<Transfer> Transfers { get; set; } = new();
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public User(string login, string passwordHash)
        {
            Id = Ulid.NewUlid();
            Name = login;
            Login = login;
            PasswordHash = passwordHash;
            RefreshToken = null;
            RefreshTokenExpiresAt = null;
            DeletedAt = null;
        }
    }
}