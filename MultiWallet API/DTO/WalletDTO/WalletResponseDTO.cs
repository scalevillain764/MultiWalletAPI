using _wallet;
namespace _wallet_response_dto
{
    public class WalletResponseDTO
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public WalletResponseDTO(Wallet wallet)
        {
            Name = wallet.Name;
            Id = wallet.Id.ToString();
            UserId = wallet.UserId.ToString();
            Currency = wallet._Currency.ToString();
            Balance = wallet.Balance;
            CreatedAt = wallet.CreatedAt;
            DeletedAt = wallet.DeletedAt;
        }
    }
}