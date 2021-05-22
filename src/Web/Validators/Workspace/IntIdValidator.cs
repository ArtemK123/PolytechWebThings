using FluentValidation;

namespace Web.Validators.Workspace
{
    public class IntIdValidator : AbstractValidator<int?>
    {
        public IntIdValidator()
        {
            RuleFor(id => id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(id => id > 0).WithMessage("Non-positive ids are not supported");
        }
    }
}