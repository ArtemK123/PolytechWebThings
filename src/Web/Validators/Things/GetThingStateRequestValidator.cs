using FluentValidation;
using Web.Models.Things.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Things
{
    public class GetThingStateRequestValidator : AbstractValidator<GetThingStateRequest>
    {
        public GetThingStateRequestValidator()
        {
            RuleFor(request => request.ThingId).NotEmpty();
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IntIdValidator());
        }
    }
}