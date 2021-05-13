using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.Workspace;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal interface IGatewayMessageSender
    {
        Task<HttpResponseMessage> SendGetThingsRequest(IWorkspace workspace);
    }
}