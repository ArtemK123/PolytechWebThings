using FluentValidation;
using Web.Models.Things.Request;

namespace Web.Validators.Things
{
    public class ChangePropertyStateRequestValidator : AbstractValidator<ChangePropertyStateRequest>
    {
        public ChangePropertyStateRequestValidator()
        {
            RuleFor(request => request.ThingId).NotEmpty();
            RuleFor(request => request.WorkspaceId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(id => id > 0).WithMessage("Non-positive workspace ids are not supported");
            RuleFor(request => request.PropertyName).NotEmpty();
        }
    }
}