namespace Domain.Entities.Workspace
{
    public record StoredWorkspaceCreationModel
    {
        public StoredWorkspaceCreationModel(int id, string name, string gatewayUrl, string userEmail)
        {
            Id = id;
            Name = name;
            GatewayUrl = gatewayUrl;
            UserEmail = userEmail;
        }

        public int Id { get; }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string UserEmail { get; }
    }
}