using Domain.Entities.WebThingsGateway;

namespace Domain.Exceptions
{
    public class InvalidPropertyValueException : ValidationException
    {
        public InvalidPropertyValueException(GatewayValueType targetValueType, string? parsedValue)
            : base($"Invalid value {parsedValue} for property with type {targetValueType}")
        {
        }
    }
}