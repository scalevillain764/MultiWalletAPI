namespace _transfer_creation_dto
{
    public class TransferCreationDTO
    {
        public string FromWalletId { get; set; }
        public string ToWalletId { get; set; }
        public TransferCreationDTO(string fromWalletId, string toWalletId)
        {
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
        }
    }
}