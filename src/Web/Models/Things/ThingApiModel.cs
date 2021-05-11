using System.Collections.Generic;

namespace Web.Models.Things
{
    public record ThingApiModel
    {
        public ThingApiModel(string title, IReadOnlyCollection<PropertyApiModel> properties)
        {
            Title = title;
            Properties = properties;
        }

        public string Title { get; }

        public IReadOnlyCollection<PropertyApiModel> Properties { get; }
    }
}