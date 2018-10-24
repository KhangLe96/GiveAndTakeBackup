using AutoMapper;
using Giveaway.API.Shared.Constants;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Models;
using Giveaway.API.Shared.Models.DTO;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Post;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DbService = Giveaway.Service.Services;
namespace Giveaway.API.Shared.Services.APIs.Realizations
{
	public class PostService<T> : IPostService<T> where T : PostBaseResponse
    {
        private readonly DbService.IPostService _postService;
        private readonly DbService.IImageService _imageService;
	    private readonly DbService.IRequestService _requestService;

		public PostService(DbService.IPostService postService, DbService.IImageService imageService, DbService.IRequestService requestService)
        {
            _postService = postService;
            _imageService = imageService;
	        _requestService = requestService;
        }

        public PagingQueryResponse<T> GetPostForPaging(IDictionary<string, string> @params, string userId, bool isListOfSingleUser)
        {
			var request = @params.ToObject<PagingQueryPostRequest>();

	        int total = 0;
			var posts = isListOfSingleUser ? GetPagedPosts(userId, request, out total) : GetPagedPosts(null, request, out total); 

			CheckIfCurrentUserRequested(userId, posts);
	        
            return new PagingQueryResponse<T>
            {
                Data = posts,
                PageInformation = new PageInformation
                {
                    Total = total,
                    Page = request.Page,
                    Limit = request.Limit
                }
            };
        }

        public T GetDetail(Guid postId, string userId)
		{
            try
            {
                var post = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity)
                    .Include(x => x.User).Include(x => x.Requests).Include(x => x.Comments).FirstAsync(x => x.Id == postId).Result;
	            //just get requests that have not deleted yet
				post.Requests = post.Requests.Where(x => x.EntityStatus != EntityStatus.Deleted).ToList();

				var postResponse = Mapper.Map<T>(post);

	            if (typeof(T) == typeof(PostAppResponse) && !string.IsNullOrEmpty(userId))
	            {
		            Guid id = Guid.Parse(userId);
		            var postAppResponse = postResponse as PostAppResponse;

					var requests = _requestService.FirstOrDefault(x =>
			            x.EntityStatus != EntityStatus.Deleted && x.PostId == post.Id && x.UserId == id);
		            if (requests != null)
		            {
			            postAppResponse.IsCurrentUserRequested = true;
		            }

		            return postAppResponse as T;
	            }

				return postResponse;
            }
            catch
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }
        }

        public PostAppResponse Create(PostRequest postRequest)
        {
            postRequest.Id = Guid.NewGuid();
            var post = Mapper.Map<Post>(postRequest);
            post.Images = null;

            _postService.Create(post, out var isPostSaved);

            if(isPostSaved)
            {
				if(postRequest.Images.Count != 0) CreateImage(postRequest);

				var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
                var postResponse = Mapper.Map<PostAppResponse>(postDb);

                return postResponse;
            }
            else
            {
                throw new InternalServerErrorException("Internal Error");
            }
        }

        public PostAppResponse Update(Guid id, PostRequest postRequest)
        {
            var post = _postService.Include(x => x.Images).FirstAsync(x => x.Id == id).Result;
            if (post == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            try
            {
                List<Image> oldImages = post.Images.ToList();
                Mapper.Map(postRequest, post);
                bool updated = _postService.Update(post);

                if (updated)
                {
                    DeleteOldImages(oldImages);
                    CreateImage(postRequest);
                }
                else
                {
                    throw new InternalServerErrorException("Internal Error");
                }

                var postDb = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity).FirstAsync(x => x.Id == post.Id).Result;
                var postResponse = Mapper.Map<PostAppResponse>(postDb);

                return postResponse;
            }
            catch
            {
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
            }
        }

        public bool ChangePostStatusCMS(Guid id, StatusRequest request)
        {
            bool updated = _postService.UpdateStatus(id, request.UserStatus) != null;
            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

        public bool ChangePostStatusApp(Guid postId, StatusRequest request)
        {
            var post = _postService.Find(postId);
            if (post == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            if (request.UserStatus == PostStatus.Giving.ToString())
            {
                post.PostStatus = PostStatus.Giving;
            }
            else if (request.UserStatus == PostStatus.Gived.ToString())
            {
                post.PostStatus = PostStatus.Gived;
            }
            else
                throw new BadRequestException(CommonConstant.Error.BadRequest);

            bool updated = _postService.Update(post);
            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

		#region Utils

		private void CheckIfCurrentUserRequested(string userId, List<T> posts)
		{
			if (typeof(T) == typeof(PostAppResponse) && !string.IsNullOrEmpty(userId))
			{
				Guid id = Guid.Parse(userId);
				foreach (PostAppResponse post in posts as List<PostAppResponse>)
				{
					var requests = _requestService.FirstOrDefault(x =>
						x.EntityStatus != EntityStatus.Deleted && x.PostId == post.Id && x.UserId == id);
					if (requests != null)
					{
						post.IsCurrentUserRequested = true;
					}
				}
			}
		}

		private void CreateImage(PostRequest post)
        {
            var imageBase64Requests = InitImageBase64Requests(post);
            var imagesDTO = ConvertFromBase64(imageBase64Requests);
            var imageDBs = InitListImageDB(post.Id, imagesDTO);

            _imageService.CreateMany(imageDBs, out var isImageSaved);

            if (!isImageSaved)
            {
                throw new InternalServerErrorException("Internal Error");
            }
        }

        private List<ImageBase64Request> InitImageBase64Requests(PostRequest post)
        {
            var requests = new List<ImageBase64Request>();
            foreach (var image in post.Images)
            {
                requests.Add(new ImageBase64Request()
                {
                    Id = post.Id.ToString(),
                    Type = "Post",
                    File = image.Image
                });
            }

            return requests;
        }

        private List<ImageDTO> ConvertFromBase64(List<ImageBase64Request> requests)
        {
            var images = new List<ImageDTO>();
            if (requests != null)
            {
                foreach (var request in requests)
                {
                    var url = UploadImageHelper.PostBase64Image(request);

                    images.Add(new ImageDTO()
                    {
                        OriginalImage = url.ElementAt(0),
                        ResizedImage = url.ElementAt(1),
                    });
                }

                return images;
            }

            return images;
        }

        private List<Image> InitListImageDB(Guid postId, List<ImageDTO> images)
        {
            var imageList = new List<Image>();
            foreach (var image in images)
            {
                imageList.Add(new Image()
                {
                    Id = Guid.NewGuid(),
                    PostId = postId,
                    OriginalImage = image.OriginalImage,
                    ResizedImage = image.ResizedImage
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
                    throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
            }
        }

        private List<T> GetPagedPosts(string userId, PagingQueryPostRequest request, out int total)
        {
			IEnumerable<Post> posts;
			try
			{
				posts = _postService.Include(x => x.Category)
					.Include(x => x.Images)
					.Include(x => x.ProvinceCity)
					.Include(x => x.User)
					.Include(x => x.Comments)
					.Include(x => x.Requests);
			}
			catch
			{
				throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
			}

			if (string.IsNullOrEmpty(userId))
			{
				posts = posts.Where(x => x.EntityStatus != EntityStatus.Deleted && x.Category.EntityStatus == EntityStatus.Activated);
			}
			else
			{
				try
				{
					Guid id = Guid.Parse(userId);
					posts = posts.Where(x => x.EntityStatus != EntityStatus.Deleted && x.UserId == id);
				}
				catch
				{
					throw new BadRequestException(CommonConstant.Error.NotFound);
				}
			}

			//filter post by properties
			posts = FilterPost(request, posts);
			posts = SortPosts(request, posts);

			//just get requests that have not deleted yet
	        posts = posts.ToList();
			foreach (var post in posts)
			{
				post.Requests = post.Requests.Where(x => x.EntityStatus != EntityStatus.Deleted).ToList();
			}

			total = posts.Count();

			return posts
				.Skip(request.Limit * (request.Page - 1))
				.Take(request.Limit)
				.Select(post => Mapper.Map<T>(post))
				.ToList();
		}

        private IEnumerable<Post> SortPosts(PagingQueryPostRequest request, IEnumerable<Post> posts)
        {
            if (!string.IsNullOrEmpty(request.Order) && request.Order == AppConstant.ASC)
            {
                posts = posts.OrderBy(x => x.CreatedTime);
            }
            else
            {
                posts = posts.OrderByDescending(x => x.CreatedTime);
            }

            return posts;
        }

        private IEnumerable<Post> FilterPost(PagingQueryPostRequest request, IEnumerable<Post> posts)
        {
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
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                posts = posts.Where(x => x.Title.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase) || 
                                         x.Description.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase) || 
                                         x.Category.CategoryName.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase) ||
                                         x.User.FullName.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase) || 
                                         x.ProvinceCity.ProvinceCityName.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase));
            }

            return posts;
        }

        #endregion
    }
}
