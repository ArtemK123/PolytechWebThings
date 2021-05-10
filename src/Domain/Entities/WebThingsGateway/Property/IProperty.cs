namespace Domain.Entities.WebThingsGateway.Property
{
    public interface IProperty
    {
        public string Name { get; }

        public string Value { get; }

        public string Href { get; }
    }
}