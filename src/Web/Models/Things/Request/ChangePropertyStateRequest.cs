namespace Web.Models.Things.Request
{
    public record ChangePropertyStateRequest
    {
        public int? WorkspaceId { get; init; }

        public string? ThingId { get; init; }

        public string? PropertyName { get; init; }

        public string? NewPropertyValue { get; init; }
    }
}