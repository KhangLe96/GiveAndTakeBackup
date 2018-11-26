using System;
using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Responses.Response;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IResponseService
	{
		ResponseRequestResponse GetResponseById(Guid id);
		ResponseRequestResponse Create(ResponseRequest response);
	}
}
