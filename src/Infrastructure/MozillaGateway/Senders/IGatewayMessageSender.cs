using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal interface IGatewayMessageSender
    {
        Task<HttpResponseMessage> GetThingsAsync(string gatewayBaseUrl, string accessToken);

        Task<HttpResponseMessage> PingAsync(string gatewayBaseUrl, string accessToken);

        Task<HttpResponseMessage> UpdatePropertyStateAsync(Property property, string serializedPayload);

        Task<HttpResponseMessage> GetPropertyStatesAsync(Thing thing);
    }
}