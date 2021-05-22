namespace PolytechWebThings.Infrastructure.Database.Rules.Steps
{
    internal class ChangePropertyStateStepDatabaseModel : StepDatabaseModel
    {
        public string ThingId { get; set; }

        public string PropertyName { get; set; }

        public string? NewPropertyState { get; set; }
    }
}