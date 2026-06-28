using _transfer;

namespace _tranfser_response_dto
{
    public class TransferResponseDTO
    {
        public string FromWalletId { get; set; }
        public string ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public TransferResponseDTO(Transfer transfer)
        {
            FromWalletId = transfer.FromWalletId.ToString();
            ToWalletId = transfer.ToWalletId.ToString();
        }
    }
}