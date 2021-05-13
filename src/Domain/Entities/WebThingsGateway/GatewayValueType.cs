using System.ComponentModel;

namespace Domain.Entities.WebThingsGateway
{
    public enum GatewayValueType
    {
        [Description("boolean")]
        Boolean,
        [Description("string")]
        String,
        [Description("enum")]
        Enum,
        [Description("number")]
        Number
    }
}