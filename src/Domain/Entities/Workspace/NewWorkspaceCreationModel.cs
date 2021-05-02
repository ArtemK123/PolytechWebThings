namespace Domain.Entities.Workspace
{
    public record NewWorkspaceCreationModel
    {
        public NewWorkspaceCreationModel(string gatewayUrl, string userEmail)
        {
            GatewayUrl = gatewayUrl;
            UserEmail = userEmail;
        }

        public string GatewayUrl { get; }

        public string UserEmail { get; }
    }
}