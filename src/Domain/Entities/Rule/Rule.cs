using System.Collections.Generic;

namespace Domain.Entities.Rule
{
    public record Rule
    {
        public Rule(int id, string name, IReadOnlyCollection<Step> steps)
        {
            Id = id;
            Name = name;
            Steps = steps;
        }

        public int Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<Step> Steps { get; }
    }
}