using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Connectors;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.UpdateWorkspace
{
    internal class UpdateWorkspaceHandler : IRequestHandler<UpdateWorkspaceCommand>
    {
        private readonly ISender mediator;
        private readonly IGatewayConnector gatewayConnector;
        private readonly IWorkspaceRepository workspaceRepository;

        public UpdateWorkspaceHandler(ISender mediator, IGatewayConnector gatewayConnector, IWorkspaceRepository workspaceRepository)
        {
            this.mediator = mediator;
            this.gatewayConnector = gatewayConnector;
            this.workspaceRepository = workspaceRepository;
        }

        public async Task<Unit> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);

            if (workspace.GatewayUrl != request.GatewayUrl)
            {
                await CheckGatewayUrlUsage(request);
            }

            bool validConnectionToGateway = await gatewayConnector.CanConnectToGatewayAsync(gatewayUrl: request.GatewayUrl, accessToken: request.AccessToken);
            if (!validConnectionToGateway)
            {
                throw new CanNotConnectToGatewayException();
            }

            IWorkspace updatedWorkspace = UpdateWorkspace(workspace, request);

            await workspaceRepository.UpdateAsync(updatedWorkspace);
            return Unit.Value;
        }

        private IWorkspace UpdateWorkspace(IWorkspace storedWorkspace, UpdateWorkspaceCommand request)
        {
            MutableWorkspace mutateModel = storedWorkspace.ToMutable() with
            {
                Name = request.Name,
                GatewayUrl = request.GatewayUrl,
                AccessToken = request.AccessToken
            };

            return storedWorkspace.Mutate(mutateModel);
        }

        private async Task CheckGatewayUrlUsage(UpdateWorkspaceCommand request)
        {
            IWorkspace? storedWorkspaceWithProvidedUrl = await workspaceRepository.GetByGatewayUrlAsync(request.GatewayUrl);
            if (storedWorkspaceWithProvidedUrl is not null)
            {
                throw new GatewayAlreadyRegisteredException(request.GatewayUrl);
            }
        }
    }
}