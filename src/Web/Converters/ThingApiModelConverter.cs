using System;
using System.Linq;
using Domain.Entities.WebThingsGateway;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Web.Models.Things;

namespace Web.Converters
{
    internal class ThingApiModelConverter : IThingApiModelConverter
    {
        public ThingApiModel Convert(Thing thing) => new ThingApiModel { Id = thing.Id, Title = thing.Title, Properties = thing.Properties.Select(Convert).ToArray() };

        private PropertyApiModel Convert(Property property)
        {
            var propertyApiModel = new PropertyApiModel
            {
                Name = property.Name,
                Visible = property.Visible,
                Title = property.Title,
                ValueType = property.ValueType,
                PropertyType = property.PropertyType,
                Links = property.Links.Select(Convert).ToArray(),
                ReadOnly = property.ReadOnly
            };

            if (property.ValueType == GatewayValueType.Boolean)
            {
                BooleanProperty convertedProperty = (BooleanProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue.ToString().ToLower() };
            }

            if (property.ValueType == GatewayValueType.Number)
            {
                NumberProperty convertedProperty = (NumberProperty)property;
                return propertyApiModel with
                {
                    DefaultValue = convertedProperty.DefaultValue.ToString(),
                    Unit = convertedProperty.Unit,
                    Minimum = convertedProperty.Minimum,
                    Maximum = convertedProperty.Maximum
                };
            }

            if (property.ValueType == GatewayValueType.String)
            {
                StringProperty convertedProperty = (StringProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue };
            }

            if (property.ValueType == GatewayValueType.Enum)
            {
                EnumProperty convertedProperty = (EnumProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue, AllowedValues = convertedProperty.AllowedValues };
            }

            throw new NotSupportedException("Unsupported property type");
        }

        private LinkApiModel Convert(Link link) => new LinkApiModel
        {
            Rel = link.Rel,
            Href = link.Href
        };
    }
}