using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Workspace;
using MediatR;

namespace Application.Queries.GetUserWorkspaces
{
    internal class GetUserWorkspacesHandler : IRequestHandler<GetUserWorkspacesQuery, IReadOnlyCollection<IWorkspace>>
    {
        private readonly IWorkspaceRepository workspaceRepository;

        public GetUserWorkspacesHandler(IWorkspaceRepository workspaceRepository)
        {
            this.workspaceRepository = workspaceRepository;
        }

        public Task<IReadOnlyCollection<IWorkspace>> Handle(GetUserWorkspacesQuery request, CancellationToken cancellationToken)
        {
            return workspaceRepository.GetByUserEmail(request.UserEmail);
        }
    }
}