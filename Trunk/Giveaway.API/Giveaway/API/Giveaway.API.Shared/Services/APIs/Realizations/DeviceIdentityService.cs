using Giveaway.API.Shared.Requests.DeviceIdentity;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
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

		public bool Create(DeviceIdentityRequest request, Guid userId)
		{
			var deviceIdentity = _deviceIdentityService.FirstOrDefault(x => x.DeviceToken == request.DeviceToken && x.EntityStatus != EntityStatus.Deleted);
			if (deviceIdentity != null)
			{
				return true;
			}

			deviceIdentity = GenerateDeviceIdentity(request, userId);

			_deviceIdentityService.Create(deviceIdentity, out var isSaved);

			if (isSaved)
			{
				return true;
			}

			throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
		}

		public bool Delete(DeviceIdentityRequest request)
		{
			if (Enum.TryParse<MobilePlatform>(request.MobilePlatform, out var platform))
			{
				var deviceIdentity = _deviceIdentityService.FirstOrDefault(x => x.DeviceToken == request.DeviceToken && x.MobilePlatform == platform);
				var isSaved = _deviceIdentityService.UpdateStatus(deviceIdentity.Id, EntityStatus.Deleted.ToString()) != null;
				if (isSaved)
				{
					return true;
				}

				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
			}

			throw new BadRequestException(CommonConstant.Error.InvalidInput);
		}

		#region Utils

		private static DeviceIdentity GenerateDeviceIdentity(DeviceIdentityRequest request, Guid userId)
		{
			DeviceIdentity deviceIdentity = new DeviceIdentity();
			if (Enum.TryParse<MobilePlatform>(request.MobilePlatform, out var platform))
			{
				deviceIdentity.MobilePlatform = platform;
			}
			else
			{
				throw new BadRequestException(CommonConstant.Error.InvalidInput);
			}
			deviceIdentity.UserId = userId;
			deviceIdentity.DeviceToken = request.DeviceToken;
			return deviceIdentity;
		}

		#endregion
	}
}
