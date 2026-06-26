using _transfer_creation_dto;
using FluentValidation;
namespace _transfer_creation_validator
{
    public class TransferCreationValidator: AbstractValidator<TransferCreationDTO>
    {
        public TransferCreationValidator()
        {
            RuleFor(x => x.FromWalletId)
                .NotEmpty().WithMessage("Номер кошелька отправителя не может быть пустым.");

            RuleFor(x => x.ToWalletId)
                .NotEmpty().WithMessage("Номер кошелька получателя не может быть пустым.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма перевода должна быть больше 0.");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Описание перевода не больше 100 символов.");
        }
    }
}