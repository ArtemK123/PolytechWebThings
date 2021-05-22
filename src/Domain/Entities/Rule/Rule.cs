using System.Collections.Generic;

namespace Domain.Entities.Rule
{
    public record Rule
    {
        public Rule(int id, string name, IReadOnlyCollection<Step> steps, int workspaceId)
        {
            Id = id;
            Name = name;
            Steps = steps;
            WorkspaceId = workspaceId;
        }

        public int Id { get; }

        public string Name { get; }

        public int WorkspaceId { get; }

        public IReadOnlyCollection<Step> Steps { get; }
    }
}