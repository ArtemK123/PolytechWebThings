namespace Domain.Entities.WebThingsGateway.Action
{
    public class ParsedActionCreationModel
    {
        public ParsedActionCreationModel(string name, string href)
        {
            Name = name;
            Href = href;
        }

        public string Name { get; }

        public string Href { get; }
    }
}