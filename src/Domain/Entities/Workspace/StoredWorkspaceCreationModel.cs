namespace Domain.Entities.Workspace
{
    public record StoredWorkspaceCreationModel
    {
        public StoredWorkspaceCreationModel(int id, string name, string gatewayUrl, string accessToken, string userEmail)
        {
            Id = id;
            Name = name;
            GatewayUrl = gatewayUrl;
            AccessToken = accessToken;
            UserEmail = userEmail;
        }

        public int Id { get; }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string AccessToken { get; }

        public string UserEmail { get; }
    }
}