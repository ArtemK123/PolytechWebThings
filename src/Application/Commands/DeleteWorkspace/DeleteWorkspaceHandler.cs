using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.DeleteWorkspace
{
    internal class DeleteWorkspaceHandler : IRequestHandler<DeleteWorkspaceCommand>
    {
        public Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}