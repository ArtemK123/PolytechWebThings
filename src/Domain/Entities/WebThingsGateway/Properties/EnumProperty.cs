using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Updaters;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record EnumProperty : StringProperty
    {
        public EnumProperty(
            string name,
            bool visible,
            string title,
            string propertyType,
            IReadOnlyCollection<Link> links,
            bool readOnly,
            IPropertyValueUpdater propertyValueUpdater,
            string defaultValue,
            IReadOnlyCollection<string> allowedValues)
            : base(name, visible, title, propertyType, links, readOnly, propertyValueUpdater, defaultValue)
        {
            AllowedValues = allowedValues;
        }

        public override GatewayValueType ValueType => GatewayValueType.Enum;

        public IReadOnlyCollection<string> AllowedValues { get; }

        public override async Task UpdateValueAsync(string? value)
        {
            if (!AllowedValues.Contains(value))
            {
                throw new ValidationException($"Value \"{value}\" is not allowed for the property. Allowed values are: [${string.Join(", ", AllowedValues)}]");
            }

            await PropertyValueUpdater.UpdateAsync(this, value);
        }
    }
}