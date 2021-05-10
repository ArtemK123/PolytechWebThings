using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.DeleteWorkspace
{
    internal class DeleteWorkspaceHandler : IRequestHandler<DeleteWorkspaceCommand>
    {
        private readonly IWorkspaceRepository workspaceRepository;

        public DeleteWorkspaceHandler(IWorkspaceRepository workspaceRepository)
        {
            this.workspaceRepository = workspaceRepository;
        }

        public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IWorkspace? workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);
            if (workspace is null)
            {
                throw new WorkspaceNotFoundByIdException(request.WorkspaceId);
            }

            if (request.UserEmail != workspace.UserEmail)
            {
                throw new UserDoesNotHaveRequiredRightsException($"Delete workspace with id={request.WorkspaceId}");
            }

            await workspaceRepository.DeleteAsync(workspace);
            return Unit.Value;
        }
    }
}