using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.CreateRule
{
    internal class CreateRuleRequestHandler : IRequestHandler<CreateRuleRequest>
    {
        public Task<Unit> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}