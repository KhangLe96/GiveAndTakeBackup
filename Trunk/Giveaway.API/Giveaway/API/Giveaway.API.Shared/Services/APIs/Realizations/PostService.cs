using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
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
            post.Id = Guid.NewGuid();

            _postService.Create(post, out var isPostSaved);
            
            //Save images of Post
            if (isPostSaved)
            {
                var imageDBs = InitImageDB(post);
                var i = _imageService.CreateMany(imageDBs, out var isImageSaved);

                if(!isImageSaved)
                {
                    throw new InternalServerErrorException("Internal Error");
                }
            } else
            {
                throw new InternalServerErrorException("Internal Error");
            }
            
            var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
            var postResponse = Mapper.Map<PostResponse>(postDb);

            return postResponse;
        }

        public bool Update(PostRequest postRequest)
        {
            var post = _postService.Find(postRequest.Id);

            Mapper.Map<PostRequest,Post>(postRequest, post);

            return _postService.Update(post);
        }

        public bool ChangePostStatusCMS(Guid id, StatusRequest request)
        {
            bool updated = _postService.UpdateStatus(id, request.UserStatus) != null;

            return updated;
        }

        public bool ChangePostStatusApp(Guid postId, StatusRequest request)
        {
            var post = _postService.Find(postId);

            if(request.UserStatus == PostStatus.Open.ToString())
            {
                post.PostStatus = PostStatus.Open;
                return _postService.Update(post);
            }
            else if(request.UserStatus == PostStatus.Close.ToString())
            {
                post.PostStatus = PostStatus.Close;
                return _postService.Update(post);
            }
            throw new BadRequestException(Const.Error.BadRequest);
        }

        #region Utils

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
