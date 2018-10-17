using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
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

        public RequestService(DbService.IRequestService requestService)
        {
            _requestService = requestService;
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

        public RequestPostResponse Create(RequestPostRequest requestRepost)
        {
            var request = Mapper.Map<Request>(requestRepost);
            request.Id = Guid.NewGuid();

            _requestService.Create(request, out var isPostSaved);
            if (isPostSaved == false)
            {
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
            }

            var requestDb = _requestService.Include(x => x.Post).Include(x => x.Response).FirstAsync(x => x.Id == request.Id).Result;
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

        public bool CheckUserRequest(Guid postId, Guid userId)
        {
            var requests = _requestService.Where(x => x.EntityStatus != EntityStatus.Deleted && x.PostId == postId && x.UserId == userId);
            if (requests.Any()) return true;

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
            var requests = _requestService.Include(x => x.Post).Include(x => x.Response).Include(x => x.User).Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (requests == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            if (!string.IsNullOrEmpty(postId))
            {
                try
                {
                    Guid id = Guid.Parse(postId);
                    requests = requests.Where(x => x.EntityStatus != EntityStatus.Deleted && x.PostId == id);
                }
                catch
                {
                    throw new BadRequestException(CommonConstant.Error.NotFound);
                }
            }

            if (!string.IsNullOrEmpty(request.RequestStatus.ToString()))
            {
                requests = requests.Where(x => x.RequestStatus == request.RequestStatus);
            }

            total = requests.Count();

            return requests
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(x => Mapper.Map<RequestPostResponse>(x))
                .ToList();
        }

        #endregion
    }
}
