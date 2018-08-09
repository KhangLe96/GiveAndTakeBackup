using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static Giveaway.Data.EF.Const;
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

        public PagingQueryResponse<PostResponse> GetPostForPaging(string userId, IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryPostRequest>();
            var posts = GetPagedPosts(userId, request);
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
                CreateImage(post);
            } else
            {
                throw new InternalServerErrorException("Internal Error");
            }
            
            var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
            var postResponse = Mapper.Map<PostResponse>(postDb);

            return postResponse;
        }

        public PostResponse Update(Guid id, PostRequest postRequest)
        {
            var post = _postService.Include(x => x.Images).FirstAsync(x => x.Id == id).Result;
            if (post == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
            }
            
            try
            {
                List<Image> oldImages = post.Images.ToList();
                Mapper.Map(postRequest, post);
                bool updated = _postService.Update(post);

                if (updated)
                {
                    DeleteOldImages(oldImages);
                    CreateImage(post);
                }
                else
                {
                    throw new InternalServerErrorException("Internal Error");
                }

                var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
                var postResponse = Mapper.Map<PostResponse>(postDb);

                return postResponse;
            }
            catch
            {
                throw new InternalServerErrorException(Const.Error.InternalServerError);
            }
        }

        public bool ChangePostStatusCMS(Guid id, StatusRequest request)
        {
            bool updated = _postService.UpdateStatus(id, request.UserStatus) != null;
            if (updated == false)
                throw new InternalServerErrorException(Error.InternalServerError);

            return updated;
        }

        public bool ChangePostStatusApp(Guid postId, StatusRequest request)
        {
            var post = _postService.Find(postId);
            if (post == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
            }

            if (request.UserStatus == PostStatus.Open.ToString())
            {
                post.PostStatus = PostStatus.Open;
            }
            else if(request.UserStatus == PostStatus.Close.ToString())
            {
                post.PostStatus = PostStatus.Close;
            }
            else
                throw new BadRequestException(Error.BadRequest);

            bool updated = _postService.Update(post);
            if (updated == false)
                throw new InternalServerErrorException(Error.InternalServerError);

            return updated;
        }

        #region Utils
        private void CreateImage(Post post)
        {
            var imageDBs = InitListImageDB(post);
            _imageService.CreateMany(imageDBs, out var isImageSaved);

            if (!isImageSaved)
            {
                throw new InternalServerErrorException("Internal Error");
            }
        }

        private List<Image> InitListImageDB(Post post)
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

        private void DeleteOldImages(List<Image> images)
        {
            foreach (var image in images)
            {
                _imageService.Delete(x => x.Id == image.Id, out var isSaved);

                if (isSaved == false)
                    throw new InternalServerErrorException(Error.InternalServerError);
            }
        }

        private List<PostResponse> GetPagedPosts(string userId, PagingQueryPostRequest request)
        {
            IEnumerable<Post> posts;
            if (string.IsNullOrEmpty(userId))
                posts = _postService.Include(x => x.Category).Include(x => x.Images).Include(x => x.ProvinceCity).Where(x => x.EntityStatus != EntityStatus.Deleted);
            else
            {
                try
                {
                    Guid id = Guid.Parse(userId);
                    posts = _postService.Include(x => x.Category).Include(x => x.Images).Include(x => x.ProvinceCity).Where(x => x.EntityStatus != EntityStatus.Deleted && x.UserId == id);
                }
                catch
                {
                    throw new BadRequestException(Error.NotFound);
                }
            }

            //filter post by properties
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
