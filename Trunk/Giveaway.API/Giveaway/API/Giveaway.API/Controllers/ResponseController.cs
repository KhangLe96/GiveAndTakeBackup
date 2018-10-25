using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Responses.Response;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/response")]
	public class ResponseController : BaseController
	{
		private readonly IResponseService _responseService;

		public ResponseController(IResponseService responseService)
		{
			_responseService = responseService;
		}

		/// <summary>
		/// Create a reponse
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("create")]
		[Produces("application/json")]
		public ResponseRequestResponse Create([FromBody]ResponseRequest request)
		{
			return _responseService.Create(request);
		}
	}
}