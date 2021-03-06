﻿using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Checkers;
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
        private readonly IGatewayConnectionChecker gatewayConnectionChecker;

        public CreateWorkspaceHandler(IWorkspaceRepository workspaceRepository, IFactory<NewWorkspaceCreationModel, IWorkspace> workspaceFactory, IGatewayConnectionChecker gatewayConnectionChecker)
        {
            this.workspaceRepository = workspaceRepository;
            this.workspaceFactory = workspaceFactory;
            this.gatewayConnectionChecker = gatewayConnectionChecker;
        }

        public async Task<Unit> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IWorkspace? workspaceByGatewayUrl = await workspaceRepository.GetByGatewayUrlAsync(request.GatewayUrl);
            if (workspaceByGatewayUrl != null)
            {
                throw new GatewayAlreadyRegisteredException(request.GatewayUrl);
            }

            bool validConnectionToGateway = await gatewayConnectionChecker.CanConnectToGatewayAsync(gatewayUrl: request.GatewayUrl, accessToken: request.AccessToken);
            if (!validConnectionToGateway)
            {
                throw new CanNotConnectToGatewayException();
            }

            IWorkspace newWorkspace = workspaceFactory.Create(
                new NewWorkspaceCreationModel(name: request.Name, gatewayUrl: request.GatewayUrl, accessToken: request.AccessToken, userEmail: request.UserEmail));
            await workspaceRepository.AddAsync(newWorkspace);
            return Unit.Value;
        }
    }
}