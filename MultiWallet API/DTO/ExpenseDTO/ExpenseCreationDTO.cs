namespace _expense_creation_dto
{
    public class ExpenseCreationDTO
    {
        public string WalletId { get; set; }
        public string Category { get; set; }
        public string ?Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseCreationDTO(string walletId, string category, string? description, decimal amount)
        {
            WalletId = walletId;
            Category = category;
            Description = description;
            Amount = amount;
        }
    }
}