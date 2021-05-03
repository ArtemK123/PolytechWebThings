using MediatR;

namespace Application.Commands.CreateWorkspace
{
    public record CreateWorkspaceCommand : IRequest
    {
        public string Name { get; }

        public string GatewayUrl { get; }

        public string AccessToken { get; }

        public string UserEmail { get; }
    }
}