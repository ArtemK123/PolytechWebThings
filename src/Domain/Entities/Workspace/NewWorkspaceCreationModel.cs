namespace Domain.Entities.Workspace
{
    public record NewWorkspaceCreationModel
    {
        public NewWorkspaceCreationModel(string name, string gatewayUrl, string accessToken, string userEmail)
        {
            Name = name;
            GatewayUrl = gatewayUrl;
            AccessToken = accessToken;
            UserEmail = userEmail;
        }

        public string Name { get; }

        public string GatewayUrl { get; }

        public string AccessToken { get; }

        public string UserEmail { get; }
    }
}