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
                = GetValueOrThrow(JsonSerializer.Deserialize<IReadOnlyCollection<ThingFlatParsingModel>>(serializedText, jsonSerializerOptions));

            IReadOnlyCollection<Thing> parsedThings = flatThingModels.Select(ParseThing).ToList();

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
            if (!propertyJson.TryGetProperty("type", out JsonElement typeElement))
            {
                throw new NotSupportedException("Cannot find type field for property");
            }

            string? propertyValueType = typeElement.GetString();

            if (propertyValueType == "boolean")
            {
                BooleanPropertyParsingModel parsedModel = GetValueOrThrow(JsonSerializer.Deserialize<BooleanPropertyParsingModel>(propertyJson.GetRawText(), jsonSerializerOptions));
                return new BooleanProperty
                {
                    Name = parsedModel.Name,
                    Visible = parsedModel.Visible,
                    Title = parsedModel.Title,
                    PropertyType = parsedModel.PropertyType,
                    Links = parsedModel.Links,
                    ReadOnly = parsedModel.ReadOnly,
                    Value = parsedModel.Value
                };
            }

            if (propertyValueType == "string")
            {
                bool isEnum = propertyJson.TryGetProperty("enum", out _);
                if (isEnum)
                {
                    var parsedModel = GetValueOrThrow(JsonSerializer.Deserialize<EnumPropertyParsingModel>(propertyJson.GetRawText(), jsonSerializerOptions));
                    return new EnumProperty
                    {
                        Name = parsedModel.Name,
                        Visible = parsedModel.Visible,
                        Title = parsedModel.Title,
                        PropertyType = parsedModel.PropertyType,
                        Links = parsedModel.Links,
                        ReadOnly = parsedModel.ReadOnly,
                        Value = parsedModel.Value
                    };
                }
                else
                {
                    var parsedModel = GetValueOrThrow(JsonSerializer.Deserialize<EnumPropertyParsingModel>(propertyJson.GetRawText(), jsonSerializerOptions));
                    return new EnumProperty
                    {
                        Name = parsedModel.Name,
                        Visible = parsedModel.Visible,
                        Title = parsedModel.Title,
                        PropertyType = parsedModel.PropertyType,
                        Links = parsedModel.Links,
                        ReadOnly = parsedModel.ReadOnly,
                        Value = parsedModel.Value,
                        AllowedValues = parsedModel.Enum
                    };
                }
            }

            if (propertyValueType == "number")
            {
                var parsedModel = GetValueOrThrow(JsonSerializer.Deserialize<NumberPropertyParsingModel>(propertyJson.GetRawText(), jsonSerializerOptions));
                return new NumberProperty
                {
                    Name = parsedModel.Name,
                    Visible = parsedModel.Visible,
                    Title = parsedModel.Title,
                    PropertyType = parsedModel.PropertyType,
                    Links = parsedModel.Links,
                    ReadOnly = parsedModel.ReadOnly,
                    Value = parsedModel.Value,
                    Unit = parsedModel.Unit,
                    Minimum = parsedModel.Minimum,
                    Maximum = parsedModel.Maximum
                };
            }

            if (propertyValueType == "integer")
            {
                var parsedModel = GetValueOrThrow(JsonSerializer.Deserialize<IntegerPropertyParsingModel>(propertyJson.GetRawText(), jsonSerializerOptions));
                return new NumberProperty
                {
                    Name = parsedModel.Name,
                    Visible = parsedModel.Visible,
                    Title = parsedModel.Title,
                    PropertyType = parsedModel.PropertyType,
                    Links = parsedModel.Links,
                    ReadOnly = parsedModel.ReadOnly,
                    Value = parsedModel.Value,
                    Unit = parsedModel.Unit,
                    Minimum = int.MinValue,
                    Maximum = int.MaxValue
                };
            }

            throw new NotSupportedException($"Unsupported property value`s type {propertyValueType}");
        }

        private TValue GetValueOrThrow<TValue>(TValue? nullableValue)
            where TValue : class
            => nullableValue ?? throw new NullReferenceException();
    }
}