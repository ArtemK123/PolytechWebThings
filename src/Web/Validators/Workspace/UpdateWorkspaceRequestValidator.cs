using FluentValidation;
using Web.Models.Workspace.Request;

namespace Web.Validators.Workspace
{
    public class UpdateWorkspaceRequestValidator : AbstractValidator<UpdateWorkspaceRequest>
    {
        public UpdateWorkspaceRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.GatewayUrl).NotEmpty();
            RuleFor(request => request.AccessToken).NotEmpty();
        }
    }
}