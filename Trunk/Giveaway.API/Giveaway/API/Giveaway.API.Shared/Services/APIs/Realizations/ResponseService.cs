﻿using AutoMapper;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Response;
using Giveaway.API.Shared.Responses.Response;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using System;
using Giveaway.Data.Enums;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class ResponseService : IResponseService
	{
		private readonly DbService.IResponseService _responseService;
		private readonly DbService.IRequestService _requestService;
		private readonly DbService.IUserService _userService;
		private readonly INotificationService _notificationService;

		public ResponseService(DbService.IResponseService responseService, DbService.IRequestService requestService, 
			INotificationService notificationService, DbService.IUserService userService)
		{
			_responseService = responseService;
			_requestService = requestService;
			_notificationService = notificationService;
			_userService = userService;
		}

		public ResponseRequestResponse GetResponseById(Guid id)
		{
			var response = _responseService.Find(id);
			if (response != null)
			{
				return Mapper.Map<ResponseRequestResponse>(response);
			}

			throw new BadRequestException(CommonConstant.Error.NotFound);
		}

		public ResponseRequestResponse Create(ResponseRequest responseRequest, Guid userId)
		{
			var response = Mapper.Map<Response>(responseRequest);
			response.Id = Guid.NewGuid();

			response = _responseService.Create(response, out var isSaved);
			if (!isSaved) throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

			var request = _requestService.Find(responseRequest.RequestId);
			if (request == null)
				throw new BadRequestException(CommonConstant.Error.NotFound);

			request.RequestStatus = RequestStatus.Approved;
			if (!_requestService.Update(request))
				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

			var user = _userService.Find(userId);
			_notificationService.Create(new Notification()
			{
				Message = $"{user.FirstName} {user.LastName} đã chấp nhận yêu cầu của bạn!",
				Type = NotificationType.IsAccepted,
				RelevantId = response.Id,
				SourceUserId = userId,
				DestinationUserId = request.UserId
			});

			var result = Mapper.Map<ResponseRequestResponse>(response);
			return result;

		}
	}
}
