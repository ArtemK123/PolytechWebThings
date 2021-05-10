using FluentValidation;

namespace Web.Validators.Workspace
{
    public class IdValidator : AbstractValidator<int?>
    {
        public IdValidator()
        {
            RuleFor(id => id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(id => id > 0).WithMessage("Non-positive ids are not supported");
        }
    }
}