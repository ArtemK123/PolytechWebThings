using FluentValidation;
using Web.Models.Workspace.Request;

namespace Web.Validators.Workspace
{
    public class CreateWorkspaceRequestValidator : AbstractValidator<CreateWorkspaceRequest>
    {
        public CreateWorkspaceRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.GatewayUrl).NotEmpty();
            RuleFor(request => request.AccessToken).NotEmpty();
        }
    }
}