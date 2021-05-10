using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.GetWorkspaceById
{
    internal class GetWorkspaceByIdHandler : IRequestHandler<GetWorkspaceByIdQuery, IWorkspace>
    {
        private readonly IWorkspaceRepository workspaceRepository;

        public GetWorkspaceByIdHandler(IWorkspaceRepository workspaceRepository)
        {
            this.workspaceRepository = workspaceRepository;
        }

        public async Task<IWorkspace> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
        {
            IWorkspace? workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);
            if (workspace is null)
            {
                throw new WorkspaceNotFoundByIdException(request.WorkspaceId);
            }

            if (request.UserEmail != workspace.UserEmail)
            {
                throw new UserDoesNotHaveRequiredRightsException($"Get workspace with id={request.WorkspaceId}");
            }

            return workspace;
        }
    }
}