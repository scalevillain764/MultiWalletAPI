using _wallet_creation_dto;
using FluentValidation;

namespace _wallet_creation_validator
{
    public class WalletCreationValidator : AbstractValidator<WalletCreationDTO>
    {
        public WalletCreationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым.")
                .MaximumLength(30).WithMessage("Максимальная длина имени - 30 символов.");

            RuleFor(x => x.CurrencyEnum)
                .IsInEnum().WithMessage("Такой категории не существует");
        }
    }
}