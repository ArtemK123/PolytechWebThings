using System.Net.Http;
using System.Threading.Tasks;
using Application.MozillaGateway.Checkers;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Checkers
{
    internal class GatewayConnectionChecker : IGatewayConnectionChecker
    {
        private readonly IGatewayMessageSender gatewayMessageSender;

        public GatewayConnectionChecker(IGatewayMessageSender gatewayMessageSender)
        {
            this.gatewayMessageSender = gatewayMessageSender;
        }

        public async Task<bool> CanConnectToGatewayAsync(string gatewayUrl, string accessToken)
        {
            try
            {
                HttpResponseMessage response = await gatewayMessageSender.SendPingRequestAsync(gatewayBaseUrl: gatewayUrl, accessToken: accessToken);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}