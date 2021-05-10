using FluentValidation;
using Web.Models.User.Request;

namespace Web.Validators
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(model => model.Email).SetValidator(new EmailValidator()).NotNull();
            RuleFor(model => model.Password).NotEmpty();
        }
    }
}