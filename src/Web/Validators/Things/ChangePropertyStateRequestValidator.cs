using FluentValidation;
using Web.Models.Things.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Things
{
    public class ChangePropertyStateRequestValidator : AbstractValidator<ChangePropertyStateRequest>
    {
        public ChangePropertyStateRequestValidator()
        {
            RuleFor(request => request.ThingId).NotEmpty();
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IntIdValidator());
            RuleFor(request => request.PropertyName).NotEmpty();
        }
    }
}