using System;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class DeviceIdentityService : IDeviceIdentityService
	{
		private readonly DbService.IDeviceIdentityService _deviceIdentityService;

		public DeviceIdentityService(DbService.IDeviceIdentityService deviceIdentityService)
		{
			_deviceIdentityService = deviceIdentityService;
		}

		public bool RegisterDevice()
		{
			throw new NotImplementedException();
		}
	}
}
