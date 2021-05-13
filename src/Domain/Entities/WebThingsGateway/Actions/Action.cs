namespace Domain.Entities.WebThingsGateway.Action
{
    internal record Action
    {
        public Action(string name, string href)
        {
            Name = name;
            Href = href;
        }

        public string Name { get; }

        public string Href { get; }
    }
}