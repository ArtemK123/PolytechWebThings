// using System.Collections.Generic;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Domain.Entities.WebThingsGateway;
//
// namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
// {
//     internal class ThingFlatParsingModel
//     {
//         public string Title { get; init; }
//
//         [JsonPropertyName("@type")]
//         public IReadOnlyCollection<string> Types { get; init; }
//
//         public string Description { get; init; }
//
//         public string Href { get; init; }
//
//         public string SelectedCapability { get; init; }
//
//         public string Id { get; init; }
//
//         public IReadOnlyCollection<Link> Links { get; init; }
//
//         public IReadOnlyDictionary<string, JsonElement> Properties { get; init; }
//     }
//
//     internal class EnumProperty : PropertyModelBase
//     {
//         public string Value { get; init; }
//
//         public IReadOnlyCollection<string> Enum { get; set; }
//     }
//
//     internal class NumberProperty : PropertyModelBase
//     {
//         public int Value { get; init; }
//
//         public string Unit { get; init; }
//
//         public int Minimum { get; init; }
//
//         public int Maximum { get; init; }
//     }
//
//     internal class IntegerProperty : PropertyModelBase
//     {
//         public int Value { get; init; }
//
//         public string Unit { get; init; }
//     }
//
//     internal class BooleanProperty : PropertyModelBase
//     {
//         public bool Value { get; init; }
//     }
//
//     internal class StringProperty : PropertyModelBase
//     {
//         public string Value { get; init; }
//     }
//
//     internal class PropertyModelBase
//     {
//         public string Name { get; init; }
//
//         public bool Visible { get; init; }
//
//         public string Title { get; init; }
//
//         [JsonPropertyName("type")]
//         public string ValueType { get; init; }
//
//         [JsonPropertyName("@type")]
//         public string PropertyType { get; init; }
//
//         public IReadOnlyCollection<LinkParsingModel> Links { get; init; }
//
//         public bool ReadOnly { get; init; }
//     }
// }