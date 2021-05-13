using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Queries.GetWorkspaceById;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using MediatR;

namespace Application.Queries.GetWorkspaceWithThings
{
    internal class GetWorkspaceWithThingsHandler : IRequestHandler<GetWorkspaceWithThingsQuery, WorkspaceWithThingsModel>
    {
        private readonly ISender mediator;
        private readonly IThingsProvider thingsProvider;

        public GetWorkspaceWithThingsHandler(ISender mediator, IThingsProvider thingsProvider)
        {
            this.mediator = mediator;
            this.thingsProvider = thingsProvider;
        }

        public async Task<WorkspaceWithThingsModel> Handle(GetWorkspaceWithThingsQuery request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);
            return new WorkspaceWithThingsModel(workspace, things);
        }
    }
}