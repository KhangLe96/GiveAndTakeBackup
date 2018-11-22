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
        private readonly DbService.IPostService _postService;

        public RequestService(DbService.IRequestService requestService, DbService.IPostService postService)
        {
            _requestService = requestService;
            _postService = postService;
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
		    var request = _requestService.Include(x => x.User).FirstOrDefault(x => x.Id == requestId);
		    if (request == null)
		    {
			    throw new BadRequestException(CommonConstant.Error.NotFound);
		    }

		    return Mapper.Map<RequestPostResponse>(request);
	    }

		public RequestPostResponse Create(RequestPostRequest requestPost)
        {
	        if (CheckWhetherUserRequested(requestPost.PostId, requestPost.UserId))
	        {
		        throw new BadRequestException(CommonConstant.Error.BadRequest);
	        }

			var request = Mapper.Map<Request>(requestPost);
			request.Id = Guid.NewGuid();

			_requestService.Create(request, out var isPostSaved);
			if (isPostSaved == false)
			{
				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
			}

			var requestDb = _requestService.Include(x => x.Post).Include(x => x.Responses).FirstOrDefault(x => x.Id == request.Id);
			var postResponse = Mapper.Map<RequestPostResponse>(requestDb);

			return postResponse;
        }

        public bool UpdateStatus(Guid requestId, StatusRequest statusRequest)
        {
            var request = _requestService.Find(requestId);
            if (request == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            ChangeStatus(statusRequest, request);

            bool updated = _requestService.Update(request);
            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

        public bool Delete(Guid requestId)
        {
            bool updated = _requestService.UpdateStatus(requestId, EntityStatus.Deleted.ToString()) != null;
            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

        public bool DeleteCurrentUserRequest(Guid postId, Guid userId)
        {
            var post = _postService.Include(x => x.Requests).Find(postId);
            foreach (var request in post.Requests)
            {
                if (request.UserId == userId && request.EntityStatus != EntityStatus.Deleted)
                {
                    return Delete(request.Id);
                }
            }

            return false;
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
            if (statusRequest.UserStatus == RequestStatus.Approved.ToString())
            {
                request.RequestStatus = RequestStatus.Approved;
            }
            else if (statusRequest.UserStatus == RequestStatus.Pending.ToString())
            {
                request.RequestStatus = RequestStatus.Pending;
            }
            else if (statusRequest.UserStatus == RequestStatus.Rejected.ToString())
            {
                request.RequestStatus = RequestStatus.Rejected;
            }
            else
                throw new BadRequestException(CommonConstant.Error.BadRequest);
        }

        private List<RequestPostResponse> GetPagedRequests(string postId, PagingQueryRequestPostRequest request, out int total)
        {
            var requests = _requestService.Include(x => x.Post).Include(x => x.Responses).Include(x => x.User).Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (requests == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            if (!string.IsNullOrEmpty(postId))
            {
                try
                {
                    Guid id = Guid.Parse(postId);
                    requests = requests.Where(x => x.PostId == id);
                }
                catch
                {
                    throw new BadRequestException(CommonConstant.Error.InvalidInput);
                }
            }

            if (!string.IsNullOrEmpty(request.RequestStatus.ToString()))
            {
                requests = requests.Where(x => x.RequestStatus == request.RequestStatus);
            }

            requests = requests.OrderByDescending(x => x.CreatedTime);
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
