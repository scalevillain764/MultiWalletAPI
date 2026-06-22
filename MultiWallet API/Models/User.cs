using _interfaces;
using _wallet;
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
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public User(string login, string passwordHash)
        {
            Id = Ulid.NewUlid();
            Name = login;
            Login = login;
            PasswordHash = passwordHash;
            RefreshToken = null;
            RefreshTokenExpiresAt = null;
        }
    }
}