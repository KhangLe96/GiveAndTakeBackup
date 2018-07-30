using Giveaway.API.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService
    {
        List<PostResponse> GetAllPost();
    }
}
