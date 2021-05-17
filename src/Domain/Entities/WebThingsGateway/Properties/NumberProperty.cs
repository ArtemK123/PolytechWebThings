using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Exceptions;
using Domain.Updaters;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record NumberProperty : Property
    {
        public NumberProperty(
            string name,
            bool visible,
            string title,
            string propertyType,
            IReadOnlyCollection<Link> links,
            bool readOnly,
            int defaultValue,
            string unit,
            int minimum,
            int maximum,
            IPropertyValueUpdater propertyValueUpdater,
            Thing thing)
            : base(name, visible, title, propertyType, links, readOnly, propertyValueUpdater, thing)
        {
            DefaultValue = defaultValue;
            Unit = unit;
            Minimum = minimum;
            Maximum = maximum;
        }

        public override GatewayValueType ValueType => GatewayValueType.Number;

        public int DefaultValue { get; }

        public string Unit { get; }

        public int Minimum { get; }

        public int Maximum { get; }

        public override async Task UpdateValueAsync(string? value)
        {
            if (!int.TryParse(value, out int parsedValue))
            {
                throw new CanNotParsePropertyValueException(ValueType, value);
            }

            if (parsedValue < Minimum || parsedValue > Maximum)
            {
                throw new ValidationException($"Parsed number ${parsedValue} is not allowed. Minimum for this value - {Minimum}, maximum - {Maximum}");
            }

            await PropertyValueUpdater.UpdateAsync(this, parsedValue);
        }
    }
}