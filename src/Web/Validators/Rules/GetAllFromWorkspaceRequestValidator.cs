using FluentValidation;
using Web.Models.Rules.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Rules
{
    public class GetAllFromWorkspaceRequestValidator : AbstractValidator<GetAllFromWorkspaceRequest>
    {
        public GetAllFromWorkspaceRequestValidator()
        {
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IntIdValidator());
        }
    }
}