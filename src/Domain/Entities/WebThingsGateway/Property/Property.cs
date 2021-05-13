namespace Domain.Entities.WebThingsGateway.Property
{
    internal record Property
    {
        public Property(string name, string value, string href)
        {
            Name = name;
            Value = value;
            Href = href;
        }

        public string Name { get; }

        public string Value { get; }

        public string Href { get; }
    }
}