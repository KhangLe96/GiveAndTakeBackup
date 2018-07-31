using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class PostService : IPostService
    {
        private readonly DbService.IPostService _postService;

        public PostService(DbService.IPostService postService)
        {
            _postService = postService;
        }
        
        public PostResponse Create(PostRequest post)
        {
            var postDb = ConvertToPostDB(post);
            _postService.Create(postDb, out var isSaved);

            if (!isSaved)
            {
                throw new SystemException("Internal Error");
            }

            return ConvertToPostResponse(postDb);
        }

        public List<PostResponse> GetAllPost()
        {
            var posts = _postService.All();

            var postResponses = new List<PostResponse>();

            foreach (var post in posts)
            {
                postResponses.Add(ConvertToPostResponse(post));
            }
            return postResponses;
        }

        #region Utils

        private Post ConvertToPostDB(PostRequest post)
        {
            return new Post()
            {
                Id = Guid.NewGuid(),
                CreatedTime     = DateTimeOffset.Now,
                UpdatedTime     = DateTimeOffset.Now,
                CategoryId      = post.CategoryId,
                Description     = post.Description,
                Title           = post.Title,
                PostStatus      = PostStatus.Open,
                ProvinceCityId  = post.ProvinceCityId,
                //UserId          = post.UserId,
                Images          = post.PostImageUrl
            };
        }

        private PostResponse ConvertToPostResponse(Post post)
        {
            return new PostResponse()
            {
                Id          = post.Id,
                CreatedTime = post.CreatedTime,
                UpdatedTime = post.UpdatedTime,
                Description = post.Description,
                Title       = post.Title,
                Category    = post.Category
            };
        }
        #endregion
    }
}
