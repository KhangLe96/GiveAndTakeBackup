using Giveaway.API.Shared.Responses;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService
    {
        List<PostResponse> GetAllPost();
    }
}
