using Domain.Entities.Common;

namespace Domain.Entities.Device
{
    public interface IDeviceFactory : IFactory<DeviceCreationModel, IDevice>
    {
    }
}