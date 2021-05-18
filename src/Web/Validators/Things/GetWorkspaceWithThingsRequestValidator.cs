using FluentValidation;
using Web.Models.Things.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Things
{
    public class GetWorkspaceWithThingsRequestValidator : AbstractValidator<GetWorkspaceWithThingsRequest>
    {
        public GetWorkspaceWithThingsRequestValidator()
        {
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IdValidator());
        }
    }
}