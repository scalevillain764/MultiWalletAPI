using _patch_wallet_dto;
using FluentValidation;
namespace _patch_wallet_validators
{
    public class WalletRenameValidator : AbstractValidator<WalletRenameDTO>
    {
        public WalletRenameValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым");
        }
    }

    public class WalletReplenishValidator: AbstractValidator<WalletReplenishDTO>
    {
        public WalletReplenishValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма пополнения должна быть больше 0");
        }
    }
}