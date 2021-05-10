namespace Domain.Entities.Workspace
{
    public record MutableWorkspace
    {
        public string Name { get; init; }

        public string GatewayUrl { get; init; }

        public string AccessToken { get; init; }

        public string UserEmail { get; init; }
    }
}