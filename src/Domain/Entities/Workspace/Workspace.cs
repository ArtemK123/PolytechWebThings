namespace Domain.Entities.Workspace
{
    internal record Workspace : IWorkspace
    {
        public Workspace(int id, string name, string gatewayUrl, string accessToken, string userEmail)
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