namespace Web.Models.Workspace.Response
{
    public record WorkspaceApiModel
    {
        public WorkspaceApiModel(int id, string name, string accessToken, string gatewayUrl)
        {
            Id = id;
            Name = name;
            AccessToken = accessToken;
            GatewayUrl = gatewayUrl;
        }

        public int Id { get; }

        public string Name { get; }

        public string AccessToken { get; }

        public string GatewayUrl { get; }
    }
}