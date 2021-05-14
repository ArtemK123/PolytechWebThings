namespace Web.Models.Things
{
    public record LinkApiModel
    {
        public string? Rel { get; init; }

        public string? Href { get; init; }
    }
}