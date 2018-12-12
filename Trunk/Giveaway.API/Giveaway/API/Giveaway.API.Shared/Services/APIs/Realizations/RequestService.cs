using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.EF.Extensions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class RequestService : IRequestService
    {
        private readonly DbService.IRequestService _requestService;
	    private readonly DbService.IUserService _userService;
		private readonly INotificationService _notificationService;
	    private readonly DbService.INotificationService _notificationDbService;

		public RequestService(DbService.IRequestService requestService, DbService.IPostService postService,
			INotificationService notificationService, DbService.IUserService userService, DbService.INotificationService notificationDbService)
        {
            _requestService = requestService;
	        _notificationService = notificationService;
	        _notificationDbService = notificationDbService;
	        _userService = userService;
        }

        public PagingQueryResponse<RequestPostResponse> GetRequestForPaging(string postId, IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryRequestPostRequest>();
            var reports = GetPagedRequests(postId, request, out var total);
            return new PagingQueryResponse<RequestPostResponse>
            {
                Data = reports,
                PageInformation = new PageInformation
                {
                    Total = total,
                    Page = request.Page,
                    Limit = request.Limit
                }
            };
        }

	    public RequestPostResponse GetRequestById(Guid requestId)
	    {
		    var request = _requestService.Include(x => x.User).Include(x => x.Post)
			    .FirstOrDefault(x => x.EntityStatus != EntityStatus.Deleted && x.Id == requestId);
		    if (request == null)
		    {
			    throw new BadRequestException(CommonConstant.Error.NotFound);
		    }

		    return Mapper.Map<RequestPostResponse>(request);
	    }

	    public RequestPostResponse GetRequestOfCurrentUserByPostId(Guid userId, Guid postId)
	    {
		    var request = _requestService.Include(x => x.User).Include(x => x.Response).Include(x => x.Post.User).Include(x => x.Post.Images)
				.FirstOrDefault(x =>
			    x.EntityStatus != EntityStatus.Deleted && 
			    x.UserId == userId && 
			    x.PostId == postId);

		    if (request == null)
		    {
			    throw new BadRequestException(CommonConstant.Error.NotFound);
		    }

		    var requestResponse = Mapper.Map<RequestPostResponse>(request);

			return requestResponse;
		}

		public RequestPostResponse Create(RequestPostRequest requestPost)
        {
			if (CheckWhetherUserRequested(requestPost.PostId, requestPost.UserId))
			{
				throw new BadRequestException(CommonConstant.Error.BadRequest);
			}

			var request = Mapper.Map<Request>(requestPost);
			request.Id = Guid.NewGuid();

			_requestService.Create(request, out var isSaved);

	        if (isSaved == false) throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

	        var requestDb = _requestService.Include(x => x.Post).Include(x => x.Response).Include(x => x.User)
		        .FirstOrDefault(x => x.Id == request.Id);

			// Send a notification to an user who requested and also save it to db
			_notificationService.Create(new Notification()
	        {
		        Message = $"{requestDb?.User.FirstName} {requestDb?.User.LastName} đã gửi yêu cầu cho bài đăng của bạn!",
		        Type = NotificationType.Request,
		        RelevantId = request.Id,
		        SourceUserId = request.UserId,
		        DestinationUserId = requestDb.Post.UserId
			});

	        var result = Mapper.Map<RequestPostResponse>(requestDb);

	        return result;
        }

        public bool UpdateStatus(Guid requestId, StatusRequest statusRequest, Guid userId)
        {
            var request = _requestService.Include(x => x.User).Find(requestId);
            if (request == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            ChangeStatus(statusRequest, request);

            bool updated = _requestService.Update(request);
            if (updated)
			{
				if (statusRequest.UserStatus == RequestStatus.Rejected.ToString())
				{
					var user = _userService.Find(userId);
					// Send a notification to an user who is rejected and also save it to db
					_notificationService.Create(new Notification()
					{
						Message = $"{user.FirstName} {user.LastName} đã từ chối yêu cầu của bạn!",
						Type = NotificationType.IsRejected,
						RelevantId = request.PostId,
						SourceUserId = userId,
						DestinationUserId = request.UserId
					});
				}

				return updated;
			}

			throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
        }

	    public bool DeleteCurrentUserRequest(Guid postId, Guid userId)
	    {
			// get a list because user can create many request with one post, it facilitate test the app easily
			// when releasing the app, we can modify to get only 1 request to consistent with create function
		    var requests = _requestService.Include(x => x.User).Include(x => x.Post)
			    .Where(x => x.EntityStatus != EntityStatus.Deleted && x.PostId == postId && x.UserId == userId);
		    if (requests.Any())
		    {
			    foreach (var request in requests)
			    {
				    if (Delete(request.Id))
				    {
					    _notificationDbService.Delete(x => x.RelevantId == request.Id, out var isSaved);
					    if (isSaved == false)
					    {
						    throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
					    }

					    if (request.RequestStatus == RequestStatus.Approved)
					    {
							// Send a notification to the post owner and also save it to db
						    _notificationService.Create(new Notification()
						    {
							    Message = $"{request.User.FirstName} {request.User.LastName} đã hủy nhận vật phẩm của bạn!",
							    Type = NotificationType.CancelRequest,
							    RelevantId = request.PostId,
							    SourceUserId = request.UserId,
							    DestinationUserId = request.Post.UserId
						    });
						}
					}
					return true;
			    }
		    }

		    throw new BadRequestException(CommonConstant.Error.NotFound);
	    }

		public bool Delete(Guid requestId)
        {
            bool updated = _requestService.UpdateStatus(requestId, EntityStatus.Deleted.ToString()) != null;
            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

        public object CheckUserRequest(Guid postId, Guid userId)
        {
            if (CheckWhetherUserRequested(postId, userId))
	            return new JsonObject("{'requested': 'true'}").Object ;

            return new JsonObject("{'requested': 'false'}").Object;
        }

	    public bool CheckIfRequestProcessed(Guid requestId)
	    {
		    var request = _requestService.FirstOrDefault(x => x.EntityStatus != EntityStatus.Deleted && x.Id == requestId && x.RequestStatus == RequestStatus.Pending);
		    if (request == null)
		    {
			    return true;
		    }

		    return false;
	    }

		#region Utils

		private void ChangeStatus(StatusRequest statusRequest, Request request)
        {
	        if (Enum.TryParse<RequestStatus>(statusRequest.UserStatus, out var status))
	        {
				request.RequestStatus = status;
			}
	        else
	        {
				throw new BadRequestException(CommonConstant.Error.InvalidInput);
			}
            
        }

        private List<RequestPostResponse> GetPagedRequests(string postId, PagingQueryRequestPostRequest request, out int total)
        {
            var requests = _requestService.Include(x => x.Post).Include(x => x.Response).Include(x => x.User)
	            .Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (requests == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }
			// Get list by postId
			if (!string.IsNullOrEmpty(postId))
            {
                try
                {
                    Guid id = Guid.Parse(postId);
                    requests = requests.Where(x => x.PostId == id && x.RequestStatus != RequestStatus.Rejected);
                }
                catch
                {
                    throw new BadRequestException(CommonConstant.Error.InvalidInput);
                }
            }

            requests = requests.OrderByDescending(x => x.RequestStatus).ThenByDescending(x => x.CreatedTime);
            total = requests.Count();

            return requests
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(x => Mapper.Map<RequestPostResponse>(x))
                .ToList();
        }

	    private bool CheckWhetherUserRequested(Guid postId, Guid userId)
	    {
		    var requests = _requestService.Where(x => x.EntityStatus != EntityStatus.Deleted && x.RequestStatus != RequestStatus.Rejected && x.PostId == postId && x.UserId == userId);
		    if (requests.Any()) return true;

		    return false;
	    }

        #endregion
    }
}
