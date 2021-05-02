namespace Domain.Entities.Workspace
{
    internal record Workspace : IWorkspace
    {
        public Workspace(string id, string gatewayUrl, string userEmail)
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