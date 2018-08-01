using System;
using System.Collections.Generic;
using AutoMapper;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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

        public List<PostResponse> GetAllPost()
        {
            var posts = _postService.Include(x => x.Category).Include(y => y.Images);
            var postResponses = Mapper.Map<List<PostResponse>>(posts);

            return postResponses;
        }

        public PostResponse Create(PostRequest postRequest)
        {
            var post = ConvertToPostDB(postRequest);
            var postDb = _postService.Create(post, out var isSaved);

            if (!isSaved)
            {
                throw new SystemException("Internal Error");
            }

            return ConvertToPostResponse(postDb);
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
                Category    = Mapper.Map<CategoryResponse>(post.Category),
                Address     = post.ProvinceCity.ProvinceCityName,
                //Images = Mapper.Map<<List<ImageResponse>>(post.Images)
            };
        }
        #endregion
    }
}
