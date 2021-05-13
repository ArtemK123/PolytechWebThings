using System;
using System.Collections.Generic;
using System.Net.Http;
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
                return await getThingsResponseParser.Parse(response);
            }
            catch (Exception exception)
            {
                throw new BrokenGatewayCommunicationException(exception);
            }
        }
    }
}