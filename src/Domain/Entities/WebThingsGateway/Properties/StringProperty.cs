using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record StringProperty : Property
    {
        public StringProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, string defaultValue)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            DefaultValue = defaultValue;
        }

        public override GatewayValueType ValueType => GatewayValueType.String;

        public string DefaultValue { get; }

        public override async Task UpdateValueAsync(string? value)
        {
            await PropertyValueUpdater.UpdateAsync(this, value);
        }
    }
}