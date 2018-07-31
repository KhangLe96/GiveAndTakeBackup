using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.API.Shared.Extensions;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class PostService : IPostService
    {
        public PostResponse Create(PostRequest post)
        {
            throw new System.NotImplementedException();
        }

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

        #region Utils

        private Post ConvertPost(PostRequest post)
        {
            return new Post()
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.Now,
                CategoryId  = post.CategoryId,
                Description = post.Description,
                Title       = post.Title,
                PostStatus  = PostStatus.Open,
                ProvinceCityId = post.ProvinceCityId,
                //UserId = User.GetUserId(); 
            };
        }

        #endregion
    }
}
