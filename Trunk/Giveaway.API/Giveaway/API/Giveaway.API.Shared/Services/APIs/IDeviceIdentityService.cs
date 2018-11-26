using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.API.Shared.Requests.DeviceIdentity;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IDeviceIdentityService
	{
		bool Create(DeviceIdentityRequest request, Guid userId);
		//bool Delete(DeviceIdentityRequest request);
	}
}
