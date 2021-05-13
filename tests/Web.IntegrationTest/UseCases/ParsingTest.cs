using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using NUnit.Framework;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace Web.IntegrationTest.UseCases
{
    public class ParsingTest
    {
        private const string GatewayUrl = "http://localhost:8080";
        private const string AccessToken = "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ijk3MWUwNTQ1LTEyZjQtNGU4Yy1hYmM1LWFiMDZlNjFmZjBlOSJ9.eyJjbGllbnRfaWQiOiJsb2NhbC10b2tlbiIsInJvbGUiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZSI6Ii90aGluZ3M6cmVhZHdyaXRlIiwiaWF0IjoxNjIwODE2MTAyLCJpc3MiOiJOb3Qgc2V0LiJ9.5JwKy58Whz29CT-Jdl3JVV0JTJhqeiya4CftgaAKQGusNfBGDULDoVhfv_IlylM-z9ibi-04lbNs454UcLLvjQ";

        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        [Test]
        public async Task GetAsync()
        {
            try
            {
                HttpResponseMessage response = await SendRequestToGatewayAsync(GatewayUrl, AccessToken);
                string responseText = await response.Content.ReadAsStringAsync();
                var result = Deserialize(responseText);
                Assert.NotNull(result);
                Assert.IsNotEmpty(result);
            }
            catch (Exception exception)
            {
                throw new BrokenGatewayCommunicationException(exception);
            }
        }

        private async Task<HttpResponseMessage> SendRequestToGatewayAsync(string gatewayUrl, string accessToken)
        {
            string thingsUrl = gatewayUrl + "/things";
            return await new HttpClient().SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
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

    internal class ThingFlatModel
    {
        public string Title { get; init; }

        [JsonPropertyName("@type")]
        public IReadOnlyCollection<string> Types { get; init; }

        public string Description { get; init; }

        public string Href { get; init; }

        public string SelectedCapability { get; init; }

        public string Id { get; init; }

        public IReadOnlyCollection<LinkParsingModel> Links { get; init; }

        public IReadOnlyDictionary<string, JsonElement> Properties { get; init; }
    }

    internal class EnumProperty : PropertyModelBase
    {
        public string Value { get; init; }

        public IReadOnlyCollection<string> Enum { get; set; }
    }

    internal class NumberProperty : PropertyModelBase
    {
        public int Value { get; init; }

        public string Unit { get; init; }

        public int Minimum { get; init; }

        public int Maximum { get; init; }
    }

    internal class IntegerProperty : PropertyModelBase
    {
        public int Value { get; init; }

        public string Unit { get; init; }
    }

    internal class BooleanProperty : PropertyModelBase
    {
        public bool Value { get; init; }
    }

    internal class StringProperty : PropertyModelBase
    {
        public string Value { get; init; }
    }

    internal class PropertyModelBase
    {
        public string Name { get; init; }

        public bool Visible { get; init; }

        public string Title { get; init; }

        [JsonPropertyName("type")]
        public string ValueType { get; init; }

        [JsonPropertyName("@type")]
        public string PropertyType { get; init; }

        public IReadOnlyCollection<LinkParsingModel> Links { get; init; }

        public bool ReadOnly { get; init; }
    }
}