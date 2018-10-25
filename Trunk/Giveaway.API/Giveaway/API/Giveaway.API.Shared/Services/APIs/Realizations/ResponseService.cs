using System;
using AutoMapper;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.API.Shared.Responses.Response;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class ResponseService : IResponseService
	{
		private readonly DbService.IResponseService _responseService;
		private readonly DbService.IRequestService _requestDbService;
		private readonly IRequestService _requestService;

		public ResponseService(DbService.IResponseService responseService, DbService.IRequestService requestDbService, IRequestService requestService)
		{
			_responseService = responseService;
			_requestDbService = requestDbService;
			_requestService = requestService;
		}

		public ResponseRequestResponse Create(ResponseRequest responseRequest)
		{
			var response = Mapper.Map<Response>(responseRequest);
			response.Id = Guid.NewGuid();

			_responseService.Create(response, out var isPostSaved);
			if (isPostSaved == false)
			{
				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
			}

			_requestService.UpdateStatus(responseRequest.RequestId, new StatusRequest(){UserStatus = "Approved"});

			var requestDb = _responseService.FirstOrDefault(x => x.Id == response.Id);
			var result = Mapper.Map<ResponseRequestResponse>(requestDb);

			return result;
		}
	}
}
