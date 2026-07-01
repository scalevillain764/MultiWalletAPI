using _interfaces;
using _transaction;
using _transfer;
using _wallet;
using System.ComponentModel.DataAnnotations.Schema;
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
        public List<Transaction> Transactions { get; set; } = new(); // np
        public List<Transfer> Transfers { get; set; } = new(); // np

        [Column(TypeName = "timestamp with time zone")]
        public DateTime? RefreshTokenExpiresAt { get; set; }

        [Column(TypeName = "timestamp with time zone")]
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