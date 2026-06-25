namespace _budget_response_dto
{
    public class BudgetResponseDTO
    {
        public string WalletId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public BudgetResponseDTO(string walletId, string category, decimal amount)
        {
            WalletId = walletId;
            Category = category;
            Amount = amount;
        }
    }
}