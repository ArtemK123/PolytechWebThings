using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingsProvider : IThingsProvider
    {
        private readonly IGatewayMessageSender gatewayMessageSender;
        private readonly IGetThingsResponseParser getThingsResponseParser;

        public ThingsProvider(IGatewayMessageSender gatewayMessageSender, IGetThingsResponseParser getThingsResponseParser)
        {
            this.gatewayMessageSender = gatewayMessageSender;
            this.getThingsResponseParser = getThingsResponseParser;
        }

        public async Task<IReadOnlyCollection<Thing>> GetAsync(IWorkspace workspace)
        {
            try
            {
                HttpResponseMessage response = await gatewayMessageSender.SendGetThingsRequestAsync(workspace.GatewayUrl, workspace.AccessToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new BrokenGatewayCommunicationException(
                        $"Invalid access token for {workspace.Name} gateway at {workspace.GatewayUrl}. Please, update access token and try again");
                }

                return await getThingsResponseParser.Parse(response);
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new BrokenGatewayCommunicationException($"Can not connect to {workspace.Name} gateway at {workspace.GatewayUrl}", httpRequestException);
            }
            catch (JsonException jsonException)
            {
                throw new BrokenGatewayCommunicationException(
                    $"{workspace.Name} gateway returned invalid data, which can not be parsed. If this error remains, please contact the support",
                    jsonException);
            }
        }
    }
}