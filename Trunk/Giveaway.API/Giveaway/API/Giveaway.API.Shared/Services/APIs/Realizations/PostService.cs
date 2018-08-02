using AutoMapper;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public bool Delete(Guid id)
        {
            var post = _postService.Find(id);
            post.IsDeleted = true;

            return _postService.Update(post);
        }

        #region Utils

        private Post InitPostDB(Post post)
        {
            post.Id = Guid.NewGuid();
            post.CreatedTime = DateTimeOffset.Now;
            post.UpdatedTime = DateTimeOffset.Now;

            //this UserId is just for test and will be removed
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

        #endregion
    }
}
