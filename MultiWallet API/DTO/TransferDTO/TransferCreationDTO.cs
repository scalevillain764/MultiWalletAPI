namespace _transfer_creation_dto
{
    public class TransferCreationDTO
    {
        public string FromWalletId { get; set; }
        public string ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransferCreationDTO(string fromWalletId, string toWalletId, decimal amount, string? description)
        {
            Amount = amount;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
            Description = description;
        }
    }
}