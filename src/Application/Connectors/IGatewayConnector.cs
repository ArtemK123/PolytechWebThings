using System.Threading.Tasks;

namespace Application.Connectors
{
    public interface IGatewayConnector
    {
        public Task<bool> CanConnectToGatewayAsync(string gatewayUrl, string accessToken);
    }
}