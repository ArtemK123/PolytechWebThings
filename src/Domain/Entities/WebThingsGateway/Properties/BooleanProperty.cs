using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record BooleanProperty : Property
    {
        public BooleanProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, bool defaultValue)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            DefaultValue = defaultValue;
        }

        public override GatewayValueType ValueType => GatewayValueType.Boolean;

        public bool DefaultValue { get; }

        public override async Task UpdateValueAsync(string? value)
        {
            if (!bool.TryParse(value, out bool parsedNewValue))
            {
                throw new CanNotParsePropertyValueException(ValueType, value);
            }

            await PropertyValueUpdater.UpdateAsync(this, parsedNewValue);
        }
    }
}