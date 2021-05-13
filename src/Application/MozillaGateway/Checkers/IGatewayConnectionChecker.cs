using System.Threading.Tasks;

namespace Application.MozillaGateway.Checkers
{
    public interface IGatewayConnectionChecker
    {
        public Task<bool> CanConnectToGatewayAsync(string gatewayUrl, string accessToken);
    }
}