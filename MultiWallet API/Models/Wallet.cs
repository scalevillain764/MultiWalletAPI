using _interfaces;
using _user;
using System.ComponentModel;
namespace _wallet
{
    public class Wallet : IEntity
    {
        public Ulid Id { get; set; }
        public Ulid UserId { get; set; } 
        public string Name { get; set; }
        public User _User { get; set; } = null; // np
        public enum Currency { USD = 1, BYN = 2, RUB = 3, EUR = 4, CNY = 5 };
        public Currency _Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public Wallet(string Name, Ulid UserId, Currency Currency)
        {
            Id = Ulid.NewUlid();
            this.Name = Name;
            this.UserId = UserId;
            _Currency = Currency;
            Balance = 0;
            CreatedAt = DateTime.UtcNow;
        }
    }
}