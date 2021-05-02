namespace Domain.Entities.Workspace
{
    public record StoredWorkspaceCreationModel
    {
        public StoredWorkspaceCreationModel(string id, string gatewayUrl, string userEmail)
        {
            Id = id;
            GatewayUrl = gatewayUrl;
            UserEmail = userEmail;
        }

        public string Id { get; }

        public string GatewayUrl { get; }

        public string UserEmail { get; }
    }
}