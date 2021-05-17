using Domain.Entities.WebThingsGateway;

namespace Domain.Exceptions
{
    public class CanNotParsePropertyValueException : ValidationException
    {
        public CanNotParsePropertyValueException(GatewayValueType targetValueType, string? parsedValue)
            : base($"Can not parse value {parsedValue} to type {targetValueType}")
        {
        }
    }
}