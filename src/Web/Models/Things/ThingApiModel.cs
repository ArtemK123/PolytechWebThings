using System.Collections.Generic;

namespace Web.Models.Things
{
    public record ThingApiModel
    {
        public string Title { get; set; }

        public IReadOnlyCollection<PropertyApiModel> Properties { get; set; }
    }
}