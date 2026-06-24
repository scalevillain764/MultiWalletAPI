using _expense_creation_dto;

namespace _expense_response_dto
{
    public class ExpenseResponseDTO
    {
        public string WalletId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public ExpenseResponseDTO(string walletId, string category, decimal amount)
        {
            WalletId = walletId;
            Category = category;
            Amount = amount;
        }
    }
}