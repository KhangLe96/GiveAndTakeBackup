using Giveaway.API.Shared.Responses;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.Data.Models.Database;
using Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService
    {
        List<PostResponse> GetAllPost();
        PostResponse Create(PostRequest post);
    }
}
