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
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingsProvider : IThingsProvider
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private readonly IHttpClientFactory httpClientFactory;

        public ThingsProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // public async Task<IReadOnlyCollection<IThing>> GetAsync(IWorkspace workspace)
        // {
        //     try
        //     {
        //         IReadOnlyCollection<ThingParsingModel> parsedThings = await GetParsedThingModelsAsync(workspace);
        //         IReadOnlyCollection<IThing> convertedThings = Convert(parsedThings);
        //         return convertedThings;
        //     }
        //     catch (Exception exception)
        //     {
        //         throw new BrokenGatewayCommunicationException(exception);
        //     }
        // }
        //
        // private async Task<IReadOnlyCollection<ThingParsingModel>> GetParsedThingModelsAsync(IWorkspace workspace)
        // {
        //     HttpResponseMessage response = await SendRequestToGatewayAsync(workspace);
        //     string responseText = await response.Content.ReadAsStringAsync();
        //     return Deserialize(responseText);
        // }
        //
        // private IReadOnlyCollection<IThing> Convert(IReadOnlyCollection<ThingParsingModel> parsingModels) => parsingModels.Select(Convert).ToList();
        //
        // private IThing Convert(ThingParsingModel thingParsingModel)
        // {
        //     // IReadOnlyCollection<IProperty> properties = thingParsingModel.Properties.Select(keyValuePair => Convert(keyValuePair.Value)).ToList();
        //     IReadOnlyCollection<IProperty> properties = Array.Empty<IProperty>();
        //     IReadOnlyCollection<IAction> actions = Array.Empty<IAction>(); // TODO: Find a way to parse actions as well
        //
        //     return thingFactory.Create(new ParsedThingCreationModel(title: thingParsingModel.Title, href: thingParsingModel.Href, properties, actions));
        // }
        //
        // private IProperty Convert(PropertyParsingModel parsingModel)
        // {
        //     return propertyFactory.Create(new ParsedPropertyCreationModel(name: parsingModel.Name, value: parsingModel.Value, href: parsingModel.Links.First().Href));
        // }
        //
        // private async Task<HttpResponseMessage> SendRequestToGatewayAsync(IWorkspace workspace)
        // {
        //     string thingsUrl = workspace.GatewayUrl + "/things";
        //     return await httpClientFactory.CreateClient().SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
        //     {
        //         Headers =
        //         {
        //             Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
        //             Authorization = new AuthenticationHeaderValue("Bearer", workspace.AccessToken)
        //         }
        //     });
        // }
        //
        // private IReadOnlyCollection<ThingParsingModel> Deserialize(string serializedText)
        // {
        //     IReadOnlyCollection<ThingParsingModel>? deserialized
        //         = JsonSerializer.Deserialize<IReadOnlyCollection<ThingParsingModel>>(serializedText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //     return deserialized ?? throw new NullReferenceException();
        // }
        public async Task<IReadOnlyCollection<Thing>> GetAsync(IWorkspace workspace)
        {
            try
            {
                HttpResponseMessage response = await SendRequestToGatewayAsync(workspace);
                string responseText = await response.Content.ReadAsStringAsync();
                var result = Deserialize(responseText);
                return Array.Empty<Thing>();
            }
            catch (Exception exception)
            {
                throw new BrokenGatewayCommunicationException(exception);
            }
        }

        private async Task<HttpResponseMessage> SendRequestToGatewayAsync(IWorkspace workspace)
        {
            string thingsUrl = workspace.GatewayUrl + "/things";
            return await httpClientFactory.CreateClient(nameof(ThingsProvider)).SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
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
            IReadOnlyCollection<ThingFlatModel>? deserialized
                = JsonSerializer.Deserialize<IReadOnlyCollection<ThingFlatModel>>(serializedText, jsonSerializerOptions);

            IReadOnlyCollection<PropertyModelBase> parsedProperties =
                deserialized.SelectMany(thing => thing.Properties.Select(keyValuePair => DeserializeProperty(keyValuePair.Value))).ToArray();
            return Array.Empty<ThingParsingModel>();
        }

        private PropertyModelBase DeserializeProperty(JsonElement propertyJson)
        {
            string propertyValueType = propertyJson.GetProperty("type").GetString();
            PropertyModelBase parsedModel = default;
            if (propertyValueType == "boolean")
            {
                parsedModel = JsonSerializer.Deserialize<BooleanProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
            }
            else if (propertyValueType == "string")
            {
                bool isEnum = propertyJson.TryGetProperty("enum", out var _);
                if (isEnum)
                {
                    parsedModel = JsonSerializer.Deserialize<EnumProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
                }
                else
                {
                    parsedModel = JsonSerializer.Deserialize<StringProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
                }
            }
            else if (propertyValueType == "number")
            {
                parsedModel = JsonSerializer.Deserialize<NumberProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
            }
            else if (propertyValueType == "integer")
            {
                parsedModel = JsonSerializer.Deserialize<IntegerProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
            }
            else
            {
                Console.WriteLine($"Unsupported property value`s type {propertyValueType}");
            }

            return parsedModel;
        }
    }
}