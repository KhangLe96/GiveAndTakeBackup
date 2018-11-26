using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
	public interface IDeviceIdentityService : IEntityService<DeviceIdentity>
	{

	}

	public class DeviceIdentityService : EntityService<DeviceIdentity>, IDeviceIdentityService
	{

	}
}
