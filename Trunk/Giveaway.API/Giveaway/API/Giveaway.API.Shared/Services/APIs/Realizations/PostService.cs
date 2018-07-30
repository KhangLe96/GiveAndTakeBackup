using System;
using System.Collections.Generic;
using System.Text;
using Giveaway.API.Shared.Responses;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class PostService : IPostService
    {
        public List<PostResponse> GetAllPost()
        {
            return new List<PostResponse>{new PostResponse()
                {
                    Title ="test",
                    Address = "daklak",
                    Description = "test",
                    PostImageUrl = "test"
                } };
        }
    }
}
