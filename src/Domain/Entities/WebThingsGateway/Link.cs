namespace Domain.Entities.WebThingsGateway
{
    public record Link
    {
        public string Rel { get; init; }

        public string Href { get; init; }
    }
}