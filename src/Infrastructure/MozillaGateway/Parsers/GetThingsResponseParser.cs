using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Converters;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class GetThingsResponseParser : IGetThingsResponseParser
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private readonly IThingParser thingParser;

        public GetThingsResponseParser(IThingParser thingParser)
        {
            this.thingParser = thingParser;
        }

        public async Task<IReadOnlyCollection<Thing>> Parse(IWorkspace workspace, HttpResponseMessage response)
        {
            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync();
            var thingParsingModels = NullableConverter.GetOrThrow(JsonSerializer.Deserialize<IReadOnlyCollection<ThingFlatParsingModel>>(responseBytes, jsonSerializerOptions));

            IReadOnlyCollection<Thing> parsedThings = thingParsingModels.Select(thingParsingModel => thingParser.Parse(thingParsingModel, workspace)).ToList();
            return parsedThings;
        }
    }
}