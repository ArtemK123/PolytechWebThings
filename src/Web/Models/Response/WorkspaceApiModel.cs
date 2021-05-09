namespace Web.Models.Response
{
    public class WorkspaceApiModel
    {
        public WorkspaceApiModel(int id, string name, string gatewayUrl)
        {
            Id = id;
            Name = name;
            GatewayUrl = gatewayUrl;
        }

        public int Id { get; }

        public string Name { get; }

        public string GatewayUrl { get; }
    }
}