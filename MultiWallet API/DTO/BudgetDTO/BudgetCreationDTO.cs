namespace _budget_creation_dto
{
    public class BudgetCreationDTO
    {
        public string WalletId { get; set; }
        public string Category { get; set; }
        public string ?Description { get; set; }
        public decimal Amount { get; set; }
        public BudgetCreationDTO(string walletId, string category, string? description, decimal amount)
        {
            WalletId = walletId;
            Category = category;
            Description = description;
            Amount = amount;
        }
    }
}