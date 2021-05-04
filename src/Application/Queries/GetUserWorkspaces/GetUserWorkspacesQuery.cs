using System.Collections.Generic;
using Domain.Entities.Workspace;
using MediatR;

namespace Application.Queries.GetUserWorkspaces
{
    public record GetUserWorkspacesQuery : IRequest<IReadOnlyCollection<IWorkspace>>
    {
        public GetUserWorkspacesQuery(string userEmail)
        {
            UserEmail = userEmail;
        }

        public string UserEmail { get; }
    }
}