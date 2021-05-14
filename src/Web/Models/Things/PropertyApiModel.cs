using System.Collections.Generic;
using Domain.Entities.WebThingsGateway;

namespace Web.Models.Things
{
    public record PropertyApiModel
    {
        public string? Name { get; init; }

        public string? Value { get; init; }

        public bool? Visible { get; init; }

        public string? Title { get; init; }

        public GatewayValueType ValueType { get; init; }

        public string? PropertyType { get; init; }

        public IReadOnlyCollection<LinkApiModel?>? Links { get; init; }

        public bool? ReadOnly { get; init; }

        public string? Unit { get; init; }

        public int? Minimum { get; init; }

        public int? Maximum { get; init; }

        public IReadOnlyCollection<string?>? AllowedValues { get; init; }
    }
}