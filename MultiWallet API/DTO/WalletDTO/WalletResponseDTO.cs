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
    }
}