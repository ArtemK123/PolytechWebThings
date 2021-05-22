namespace PolytechWebThings.Infrastructure.Database.Rules.Steps
{
    internal class ChangeThingStateStepDatabaseModel : StepDatabaseModel
    {
        public string ThingId { get; set; } = null!;

        public string PropertyName { get; set; } = null!;

        public string? NewPropertyState { get; set; }
    }
}