namespace Domain.Entities.Workspace
{
    public interface IWorkspace
    {
        int Id { get; }

        string Name { get; }

        string GatewayUrl { get; }

        string UserEmail { get; }
    }
}