namespace Domain.Entities.WebThingsGateway
{
    public record Link
    {
        public Link(string rel, string href)
        {
            Rel = rel;
            Href = href;
        }

        public string Rel { get; }

        public string Href { get; }
    }
}