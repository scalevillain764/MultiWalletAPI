using _user_registration_login_request_dto;
using FluentValidation;
namespace _user_registration_login_request_validator
{
    public class UserRegistrationandLogInRequestValidator : AbstractValidator<UserRegistrationandLogInRequestDTO>
    {
        public UserRegistrationandLogInRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым.")
                .MinimumLength(4).WithMessage("Минимальная длина логина — 4 символа.")
                .MaximumLength(20).WithMessage("Максимальная длина логина — 20 символов.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Логин может содержать только латинские буквы и цифры.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Логин не может быть пустым.")
                .MinimumLength(6).WithMessage("Минимальная длина пароля - 6 символов.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Пароль может содержать только латинские буквы и цифры.");
        }
    }
}