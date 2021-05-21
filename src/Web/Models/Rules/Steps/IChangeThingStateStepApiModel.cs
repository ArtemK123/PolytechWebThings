namespace Web.Models.Rules.Steps
{
    public interface IChangeThingStateStepApiModel : IStepApiModel
    {
        public string? ThingId { get; init; }

        public string? PropertyName { get; init; }

        public string? NewPropertyState { get; init; }
    }
}