namespace Domain.Entities.WebThingsGateway.Thing
{
    public record Thing
    {
        public Thing(string title, string href)
        {
            Title = title;
            Href = href;
        }

        public string Title { get; init; }

        public string Href { get; init; }
    }
}