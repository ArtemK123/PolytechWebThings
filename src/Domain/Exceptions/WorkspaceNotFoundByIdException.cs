namespace Domain.Exceptions
{
    public class WorkspaceNotFoundByIdException : EntityNotFoundException
    {
        public WorkspaceNotFoundByIdException(int workspaceId)
            : base($"Workspace with id={workspaceId} is not found")
        {
        }
    }
}