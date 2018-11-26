using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.DeviceIdentity;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/deviceIdentity")]
	public class DeviceIdentityController : BaseController
	{
		private readonly IDeviceIdentityService _deviceIdentityService;

		public DeviceIdentityController(IDeviceIdentityService deviceIdentityService)
		{
			_deviceIdentityService = deviceIdentityService;
		}

		/// <summary>
		/// Register a device to receive notifications
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("registerDevice")]
		[Produces("application/json")]
		public bool RegisterDevice([FromBody] DeviceIdentityRequest request)
		{
			var userId = User.GetUserId();
			return _deviceIdentityService.Create(request, userId);
		}

		//[Authorize]
		//[HttpDelete("delete")]
		//[Produces("application/json")]
		//public bool Delete([FromBody] DeviceIdentityRequest request)
		//{
		//	return _deviceIdentityService.Delete(request);
		//}
	}
}
