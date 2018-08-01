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
        private readonly DbService.IImageService _imageService;

        public PostService(DbService.IPostService postService, DbService.IImageService imageService)
        {
            _postService = postService;
            _imageService = imageService;
        }

        public List<PostResponse> GetAllPost()
        {
            var posts = _postService.Include(x => x.Category).Include(y => y.Images);
            var postResponses = Mapper.Map<List<PostResponse>>(posts);

            return postResponses;
        }

        public PostResponse Create(PostRequest postRequest)
        {
            //var post = ConvertToPostDB(postRequest);
            var post = Mapper.Map<Post>(postRequest);
            post = InitPostDB(post);

            var postDb = _postService.Create(post, out var isPostSaved);
            _imageService.CreateMany(InitImageDB(postDb), out var isImageSaved);
            if (!isPostSaved || !isImageSaved)
            {
                throw new SystemException("Internal Error");
            }

            var postResponse = Mapper.Map<PostResponse>(postDb);

            return postResponse;
        }

        #region Utils

        private Post InitPostDB(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreatedTime = DateTimeOffset.Now;
            post.UpdatedTime = DateTimeOffset.Now;

            //this UserId is just for test and will be removed
            post.UserId = Guid.Parse("4fa442c8-44e1-425d-b63d-20c2e2ba957d");

            return post;
        }

        private List<Image> InitImageDB(Post post)
        {
            var imageList = new List<Image>();
            foreach(var image in post.Images)
            {
                imageList.Add(new Image()
                {
                    Id = Guid.NewGuid(),
                    PostId = post.Id,
                    ImageUrl = image.ImageUrl,
                });
            }

            return imageList;
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
