namespace PolytechWebThings.Infrastructure.Database.Rules.Steps
{
    internal class ChangeThingStateStepDatabaseModel : StepDatabaseModel
    {
        public string ThingId { get; set; }

        public string PropertyName { get; set; }

        public string? NewPropertyState { get; set; }
    }
}