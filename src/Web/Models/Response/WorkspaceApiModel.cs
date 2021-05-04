namespace Web.Models.Response
{
    public class WorkspaceApiModel
    {
        public WorkspaceApiModel(string name, string gatewayUrl)
        {
            Name = name;
            GatewayUrl = gatewayUrl;
        }

        public string Name { get; }

        public string GatewayUrl { get; }
    }
}