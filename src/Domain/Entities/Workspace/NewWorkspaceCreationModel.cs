namespace Domain.Entities.Workspace
{
    public record NewWorkspaceCreationModel
    {
        public NewWorkspaceCreationModel(string name, string gatewayUrl, string userEmail)
        {
            Name = name;
            GatewayUrl = gatewayUrl;
            UserEmail = userEmail;
        }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string UserEmail { get; }
    }
}