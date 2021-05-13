using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.Common;
using Domain.Entities.WebThingsGateway.Action;
using Domain.Entities.WebThingsGateway.Property;
using Domain.Entities.WebThingsGateway.Thing;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingsProvider : IThingsProvider
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IFactory<ParsedThingCreationModel, IThing> thingFactory;
        private readonly IFactory<ParsedPropertyCreationModel, IProperty> propertyFactory;

        public ThingsProvider(
            IHttpClientFactory httpClientFactory,
            IFactory<ParsedThingCreationModel, IThing> thingFactory,
            IFactory<ParsedPropertyCreationModel, IProperty> propertyFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.thingFactory = thingFactory;
            this.propertyFactory = propertyFactory;
        }

        public async Task<IReadOnlyCollection<IThing>> GetAsync(IWorkspace workspace)
        {
            try
            {
                IReadOnlyCollection<ThingParsingModel> parsedThings = await GetParsedThingModelsAsync(workspace);
                IReadOnlyCollection<IThing> convertedThings = Convert(parsedThings);
                return convertedThings;
            }
            catch (Exception exception)
            {
                throw new BrokenGatewayCommunicationException(exception);
            }
        }

        private async Task<IReadOnlyCollection<ThingParsingModel>> GetParsedThingModelsAsync(IWorkspace workspace)
        {
            HttpResponseMessage response = await SendRequestToGatewayAsync(workspace);
            string responseText = await response.Content.ReadAsStringAsync();
            return Deserialize(responseText);
        }

        private IReadOnlyCollection<IThing> Convert(IReadOnlyCollection<ThingParsingModel> parsingModels) => parsingModels.Select(Convert).ToList();

        private IThing Convert(ThingParsingModel thingParsingModel)
        {
            // IReadOnlyCollection<IProperty> properties = thingParsingModel.Properties.Select(keyValuePair => Convert(keyValuePair.Value)).ToList();
            IReadOnlyCollection<IProperty> properties = Array.Empty<IProperty>();
            IReadOnlyCollection<IAction> actions = Array.Empty<IAction>(); // TODO: Find a way to parse actions as well

            return thingFactory.Create(new ParsedThingCreationModel(title: thingParsingModel.Title, href: thingParsingModel.Href, properties, actions));
        }

        private IProperty Convert(PropertyParsingModel parsingModel)
        {
            return propertyFactory.Create(new ParsedPropertyCreationModel(name: parsingModel.Name, value: parsingModel.Value, href: parsingModel.Links.First().Href));
        }

        private async Task<HttpResponseMessage> SendRequestToGatewayAsync(IWorkspace workspace)
        {
            string thingsUrl = workspace.GatewayUrl + "/things";
            return await httpClientFactory.CreateClient().SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", workspace.AccessToken)
                }
            });
        }

        private IReadOnlyCollection<ThingParsingModel> Deserialize(string serializedText)
        {
            IReadOnlyCollection<ThingParsingModel>? deserialized
                = JsonSerializer.Deserialize<IReadOnlyCollection<ThingParsingModel>>(serializedText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return deserialized ?? throw new NullReferenceException();
        }
    }
}