namespace Web.Models.Rules.Steps
{
    public record ChangeThingStateStepApiModel : StepApiModel
    {
        public string? ThingId { get; init; }

        public string? PropertyName { get; init; }

        public string? NewPropertyState { get; init; }
    }
}