using System.Collections.Generic;

namespace Domain.Entities.Rule.Models
{
    public record RuleCreationModel
    {
        public RuleCreationModel(int workspaceId, string name, IReadOnlyCollection<StepModel> steps)
        {
            WorkspaceId = workspaceId;
            Name = name;
            Steps = steps;
        }

        public int WorkspaceId { get; }

        public string Name { get; }

        public IReadOnlyCollection<StepModel> Steps { get; }
    }
}