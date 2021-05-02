namespace Domain.Entities.Workspace
{
    public interface IWorkspace
    {
        string Id { get; }

        string GatewayUrl { get; }

        string UserEmail { get; }
    }
}