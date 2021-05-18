using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal interface IGatewayMessageSender
    {
        Task<HttpResponseMessage> GetThingsAsync(string gatewayBaseUrl, string accessToken);

        Task<HttpResponseMessage> PingAsync(string gatewayBaseUrl, string accessToken);

        Task<HttpResponseMessage> UpdatePropertyStateAsync(Property property, string serializedPayload);
    }
}