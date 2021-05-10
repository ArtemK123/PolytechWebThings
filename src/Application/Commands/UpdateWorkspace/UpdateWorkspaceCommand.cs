using MediatR;

namespace Application.Commands.UpdateWorkspace
{
    public class UpdateWorkspaceCommand : IRequest
    {
        public UpdateWorkspaceCommand(int workspaceId, string name, string gatewayUrl, string accessToken, string userEmail)
        {
            WorkspaceId = workspaceId;
            Name = name;
            GatewayUrl = gatewayUrl;
            AccessToken = accessToken;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string AccessToken { get; }

        public string UserEmail { get; }
    }
}