using System;

namespace Domain.Exceptions
{
    public class WorkspaceNotFoundByIdException : Exception
    {
        public WorkspaceNotFoundByIdException(int workspaceId)
            : base($"Workspace with id={workspaceId} is not found")
        {
        }
    }
}