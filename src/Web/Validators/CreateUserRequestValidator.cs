using FluentValidation;
using Web.Models.Request;

namespace Web.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(model => model.Email).SetValidator(validator: new EmailValidator()).NotNull();
            RuleFor(model => model.Password).NotEmpty();
        }
    }
}