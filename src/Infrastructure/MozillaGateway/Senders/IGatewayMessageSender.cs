using System.Net.Http;
using System.Threading.Tasks;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal interface IGatewayMessageSender
    {
        Task<HttpResponseMessage> SendGetThingsRequestAsync(string gatewayBaseUrl, string accessToken);

        Task<HttpResponseMessage> SendPingRequestAsync(string gatewayBaseUrl, string accessToken);
    }
}