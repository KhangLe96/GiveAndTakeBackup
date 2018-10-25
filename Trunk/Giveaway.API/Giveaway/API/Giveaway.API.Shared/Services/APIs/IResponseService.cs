using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Responses.Response;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IResponseService
	{
		ResponseRequestResponse Create(ResponseRequest response);
	}
}
