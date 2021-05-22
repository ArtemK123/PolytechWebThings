namespace PolytechWebThings.Infrastructure.Database.Rules.Steps
{
    internal abstract class StepDatabaseModel
    {
        public int Id { get; set; }

        public int RuleId { get; set; }

        public RuleDatabaseModel Rule { get; set; } = null!;

        public int ExecutionOrderPosition { get; set; }
    }
}