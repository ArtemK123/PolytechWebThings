using System.Collections.Generic;
using Domain.Entities.Rule;
using MediatR;

namespace Application.Queries.GetRulesFromWorkspace
{
    public record GetRulesFromWorkspaceQuery : IRequest<IReadOnlyCollection<Rule>>
    {
        public GetRulesFromWorkspaceQuery(int workspaceId, string userEmail)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }
    }
}