using System.Collections.Generic;

namespace Domain.Entities.Rule.Models
{
    public record RuleCreationModel
    {
        public RuleCreationModel(string name, IReadOnlyCollection<StepCreationModel> steps)
        {
            Name = name;
            Steps = steps;
        }

        public string Name { get; }

        public IReadOnlyCollection<StepCreationModel> Steps { get; }
    }
}