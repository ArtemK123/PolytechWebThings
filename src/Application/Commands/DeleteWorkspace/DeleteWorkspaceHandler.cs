using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Workspace;
using MediatR;

namespace Application.Commands.DeleteWorkspace
{
    internal class DeleteWorkspaceHandler : IRequestHandler<DeleteWorkspaceCommand>
    {
        private readonly IWorkspaceRepository workspaceRepository;
        private readonly ISender mediator;

        public DeleteWorkspaceHandler(IWorkspaceRepository workspaceRepository, ISender mediator)
        {
            this.workspaceRepository = workspaceRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);

            await workspaceRepository.DeleteAsync(workspace);
            return Unit.Value;
        }
    }
}