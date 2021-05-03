using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Common;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.CreateWorkspace
{
    internal class CreateWorkspaceHandler : IRequestHandler<CreateWorkspaceCommand>
    {
        private readonly IWorkspaceRepository workspaceRepository;
        private readonly IFactory<NewWorkspaceCreationModel, IWorkspace> workspaceFactory;

        public CreateWorkspaceHandler(IWorkspaceRepository workspaceRepository, IFactory<NewWorkspaceCreationModel, IWorkspace> workspaceFactory)
        {
            this.workspaceRepository = workspaceRepository;
            this.workspaceFactory = workspaceFactory;
        }

        public async Task<Unit> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IWorkspace? workspaceByGatewayUrl = await workspaceRepository.GetByGatewayUrlAsync(request.GatewayUrl);
            if (workspaceByGatewayUrl != null)
            {
                throw new GatewayAlreadyRegisteredException(request.GatewayUrl);
            }

            IWorkspace newWorkspace = workspaceFactory.Create(new NewWorkspaceCreationModel(name: request.Name, gatewayUrl: request.GatewayUrl, userEmail: request.UserEmail));
            await workspaceRepository.AddAsync(newWorkspace);
            return Unit.Value;
        }
    }
}