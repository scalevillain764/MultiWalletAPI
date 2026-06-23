using _transfer;

namespace _tranfser_response_dto
{
    public class TransferResponseDTO
    {
        public string FromUserId { get; set; }
        public string FromWalletId { get; set; }
        public string ToUserId { get; set; }
        public string ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public TransferResponseDTO(Transfer transfer)
        {
            FromUserId = transfer.FromUserId.ToString();
            FromWalletId = transfer.FromWalletId.ToString();
            ToUserId = transfer.ToUserId.ToString();
            ToWalletId = transfer.ToWalletId.ToString();
        }
    }
}