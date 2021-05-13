using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway.Properties;
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

        public async Task<IReadOnlyCollection<Thing>> GetAsync(IWorkspace workspace)
        {
            try
            {
                HttpResponseMessage response = await SendRequestToGatewayAsync(workspace);
                string responseText = await response.Content.ReadAsStringAsync();
                IReadOnlyCollection<Thing> parsedThings = Deserialize(responseText);
                return parsedThings;
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

        private IReadOnlyCollection<Thing> Deserialize(string serializedText)
        {
            IReadOnlyCollection<ThingFlatParsingModel> flatThingModels
                = JsonSerializer.Deserialize<IReadOnlyCollection<ThingFlatParsingModel>>(serializedText, jsonSerializerOptions)
                  ?? throw new NullReferenceException();

            IReadOnlyCollection<Thing> parsedThings = flatThingModels.Select(ParseThing).ToList();

            // IReadOnlyCollection<PropertyModelBase> parsedProperties =
            //     flatThingModels.SelectMany(thing => thing.Properties.Select(keyValuePair => DeserializeProperty(keyValuePair.Value))).ToArray();
            return parsedThings;
        }

        private Thing ParseThing(ThingFlatParsingModel flatThingModel)
        {
            IReadOnlyCollection<Property> parsedProperties = flatThingModel.Properties.Select(keyValuePair => ParseProperty(keyValuePair.Value)).ToArray();

            return new Thing
            {
                Title = flatThingModel.Title,
                Types = flatThingModel.Types,
                Description = flatThingModel.Description,
                Href = flatThingModel.Href,
                SelectedCapability = flatThingModel.SelectedCapability,
                Id = flatThingModel.Id,
                Links = flatThingModel.Links,
                Properties = parsedProperties
            };
        }

        private Property ParseProperty(JsonElement propertyJson)
        {
            string propertyValueType = propertyJson.GetProperty("type").GetString();
            Property parsedModel = default;
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
                parsedModel = JsonSerializer.Deserialize<NumberProperty>(propertyJson.GetRawText(), jsonSerializerOptions);
            }
            else
            {
                Console.WriteLine($"Unsupported property value`s type {propertyValueType}");
            }

            return parsedModel;
        }
    }
}