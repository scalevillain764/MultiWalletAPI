using _user;
namespace _transfer
{
    public class Transfer
    {
        public Ulid FromId { get; set; }
        public User FromUser { get; set; } = null;
        public Ulid ToId { get; set; }
        public User ToUser { get; set; } = null;
        public decimal Amount { get; set; }
        public Transfer(Ulid fromId, User fromUser, Ulid toId, User toUser, decimal amount)
        {
            FromId = fromId;
            FromUser = fromUser;
            ToId = toId;
            ToUser = toUser;
            Amount = amount;
        }
    }
}