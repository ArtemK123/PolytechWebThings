using Application.Users.Validators;
using FluentValidation;

namespace Application.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(model => model.Email).SetValidator(new EmailValidator()).NotNull();
            RuleFor(model => model.Password).NotEmpty();
        }
    }
}