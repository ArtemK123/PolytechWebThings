namespace Web.Models.Things
{
    public record PropertyApiModel
    {
        public string? Name { get; init; }

        public string? Value { get; init; }

        public string? ValueType { get; init; }
    }
}