namespace Web.Models.Things.Request
{
    public record GetThingStateRequest
    {
        public string? ThingId { get; init; }

        public int? WorkspaceId { get; init; }
    }
}