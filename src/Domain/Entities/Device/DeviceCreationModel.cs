namespace Domain.Entities.Device
{
    public record DeviceCreationModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string Url { get; init; }
    }
}