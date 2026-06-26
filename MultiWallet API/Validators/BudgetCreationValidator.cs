using _budget_creation_dto;
using FluentValidation;
namespace _budget_creation_validator
{
    public class BudgetCreationValidator : AbstractValidator<BudgetCreationDTO>
    {
        public BudgetCreationValidator()
        {
            RuleFor(x => x.WalletId)
                .NotEmpty().WithMessage("Номер кошелька не может быть пустым.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Неизвестная категория");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма операции должна быть больше 0");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Описание операции не больше 100 символов.");
        }
    }
}