using Domain.Entities.Device;

namespace Domain.Entities
{
    public record Function
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Url { get; init; }

        public IDevice Device { get; init; }
    }
}