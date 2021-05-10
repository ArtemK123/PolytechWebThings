using FluentValidation;
using Web.Models.Workspace.Request;

namespace Web.Validators.Workspace
{
    public class DeleteWorkspaceRequestValidator : AbstractValidator<DeleteWorkspaceRequest>
    {
        public DeleteWorkspaceRequestValidator()
        {
            RuleFor(request => request.WorkspaceId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(id => id > 0).WithMessage("Non-positive ids are not supported");
        }
    }
}