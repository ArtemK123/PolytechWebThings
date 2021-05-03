using MediatR;

namespace Application.Commands.CreateWorkspace
{
    public record CreateWorkspaceCommand : IRequest
    {
        public CreateWorkspaceCommand(string name, string gatewayUrl, string accessToken, string userEmail)
        {
            Name = name;
            GatewayUrl = gatewayUrl;
            AccessToken = accessToken;
            UserEmail = userEmail;
        }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string AccessToken { get; }

        public string UserEmail { get; }
    }
}