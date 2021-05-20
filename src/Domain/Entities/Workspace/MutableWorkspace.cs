namespace Domain.Entities.Workspace
{
    public record MutableWorkspace
    {
        public string Name { get; init; } = default!;

        public string GatewayUrl { get; init; } = default!;

        public string AccessToken { get; init; } = default!;

        public string UserEmail { get; init; } = default!;
    }
}