using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using static Giveaway.Data.EF.Const;
using DbService = Giveaway.Service.Services;
//Remove namespace is unused
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

        public PagingQueryResponse<PostResponse> GetPostForPaging(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryPostRequest>();
            var posts = GetPagedPosts(request);
            return new PagingQueryResponse<PostResponse>
            {
                Data = posts,
                PageInformation = new PageInformation
                {
                    Total = _postService.Count(),
                    Page = request.Page,
                    Limit = request.Limit
                }
            };
        }

        public PostResponse Create(PostRequest postRequest)
        {
            var post = Mapper.Map<Post>(postRequest);
            post = InitPostDB(post);

            _postService.Create(post, out var isPostSaved);
            
            if (isPostSaved)
            {
                var imageDBs = InitImageDB(post);
                var i = _imageService.CreateMany(imageDBs, out var isImageSaved);

                if(!isImageSaved)
                {
                    throw new SystemException("Internal Error");
                }
            } else
            {
                throw new SystemException("Internal Error");
            }
            
            var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
            var postResponse = Mapper.Map<PostResponse>(postDb);

            return postResponse;
        }

        public bool Update(PostRequest postRequest)
        {
            var post = Mapper.Map<Post>(postRequest);
            //if you implement like this, createdTime will be updated with DateTime.Now => wrong
            //this UserId is just for test and will be got after user has logined
            post.UserId = Guid.Parse("5151357e-bb71-4e7f-bfaf-ecc6944cc94f");
            //Review: Should get object from db and update some fields. 
            return _postService.Update(post);
        }

        public bool Delete(Guid id)
        {
            var post = _postService.Find(id);
            if(post != null)
                post.EntityStatus = EntityStatus.Deleted;

            return _postService.Update(post);
        }

        #region Utils

        private Post InitPostDB(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreatedTime = DateTimeOffset.Now;
            post.UpdatedTime = DateTimeOffset.Now;

            //this UserId is just for test and will be got after user has logined
            post.UserId = Guid.Parse("45f22de6-d5c8-4a7c-95b6-6828d6430c70");

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

        private List<PostResponse> GetPagedPosts(PagingQueryPostRequest request)
        {
            var posts = _postService.Include(x => x.Category).Include(x => x.Images).Include(x => x.ProvinceCity).Where(x => x.EntityStatus != EntityStatus.Deleted);
            //Review: should have more params to query such as CreatedTime, provinceCityId, categoryId
            if (!string.IsNullOrEmpty(request.Title))
            {
                posts = posts.Where(x => x.Title.Contains(request.Title));
            }
            if (!string.IsNullOrEmpty(request.ProvinceCityId))
            {
                posts = posts.Where(x => x.ProvinceCityId.Equals(Guid.Parse(request.ProvinceCityId)));
            }
            if (!string.IsNullOrEmpty(request.CategoryId)) 
            {
                posts = posts.Where(x => x.CategoryId.Equals(Guid.Parse(request.CategoryId)));
            }
            return posts
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(post => Mapper.Map<PostResponse>(post))
                .ToList();
        }
        #endregion
    }
}
