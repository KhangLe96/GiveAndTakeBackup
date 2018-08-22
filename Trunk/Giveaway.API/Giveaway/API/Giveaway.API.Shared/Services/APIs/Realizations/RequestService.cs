using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static Giveaway.Data.EF.Const;
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

        public PagingQueryResponse<RequestPostResponse> GetRequesttForPaging(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryRequestPostRequest>();
            var reports = GetPagedRequests(request, out var total);
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
            if(isPostSaved == false)
            {
                throw new InternalServerErrorException(Error.InternalServerError);
            }
            
            var requestDb = _requestService.Include(x => x.Post).Include(x => x.Response).FirstAsync(x => x.Id == request.Id).Result;
            var postResponse = Mapper.Map<RequestPostResponse>(requestDb);

            return postResponse;
        }

        #region Utils

        private List<RequestPostResponse> GetPagedRequests(PagingQueryRequestPostRequest request, out int total)
        {
            var requests = _requestService.Include(x => x.Post).Include(x => x.Response).Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (requests == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
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
