// using System.Collections.Generic;
// using Domain.Entities.WebThingsGateway.Action;
// using Domain.Entities.WebThingsGateway.Property;
//
// namespace Domain.Entities.WebThingsGateway.Thing
// {
//     public record ParsedThingCreationModel
//     {
//         public ParsedThingCreationModel(string title, string href, IReadOnlyCollection<IProperty> properties, IReadOnlyCollection<IAction> actions)
//         {
//             Title = title;
//             Href = href;
//             Properties = properties;
//             Actions = actions;
//         }
//
//         public string Title { get; }
//
//         public string Href { get; }
//
//         public IReadOnlyCollection<IProperty> Properties { get; }
//
//         public IReadOnlyCollection<IAction> Actions { get; }
//     }
// }