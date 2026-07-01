using _interfaces;
using _user;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace _wallet
{
    public class Wallet : IEntity
    {
        public enum Currency { USD = 1, BYN = 2, RUB = 3, EUR = 4, CNY = 5 };

        public Ulid Id { get; set; }

        public Ulid UserId { get; set; }
        public User _User { get; set; } = null; // np

        public string Name { get; set; }

        public Currency _Currency { get; set; }
        public decimal Balance { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime? DeletedAt { get; set; }
        public Wallet(string Name, Ulid UserId, Currency Currency)
        {
            Id = Ulid.NewUlid();
            this.Name = Name;
            this.UserId = UserId;
            _Currency = Currency;
            Balance = 0;
            CreatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }
    }
}