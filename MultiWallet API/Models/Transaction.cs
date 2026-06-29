using _category;
using _interfaces;
using _user;
using _wallet;
using System.ComponentModel.DataAnnotations.Schema;
namespace _transaction
{
    public class Transaction : IEntity
    {
        public enum TransactionType { Income = 1, Expense = 2, Transfer = 3, Deposit = 4 /*через ПС*/}
        public enum TransactionStatus { Pending = 1, Completed = 2, Failed = 3, Cancelled = 4} 
        public Ulid Id { get; set; }
        public Ulid UserId { get; set; }
        public User User { get; set; } = null;
        public Ulid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }

        [Column(TypeName = "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone")]
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public Transaction(Ulid userId, Ulid walletId, decimal amount, TransactionType type, string? description, Category? category)
        {
            Id = Ulid.NewUlid();
            UserId = userId;
            WalletId = walletId;
            Amount = amount;
            Type = type;
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            Description = description;
            Category = category;
        }
        private Transaction()
        {
        }
    }
}