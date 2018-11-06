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

	        int total;
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

	    public PagingQueryResponse<PostAppResponse> GetListRequestedPostOfUser(IDictionary<string, string> @params, string userId)
	    {
		    var request = @params.ToObject<PagingQueryPostRequest>();

		    if (Guid.TryParse(userId, out var id))
		    {
				var posts = _postService.Include(x => x.Category)
					.Include(x => x.Images)
					.Include(x => x.ProvinceCity)
					.Include(x => x.User)
					.Include(x => x.Comments)
					.Include(x => x.Requests)
					.Where(x => x.Category.EntityStatus == EntityStatus.Activated);

			    IEnumerable<Post> requestedPosts = new List<Post>();
				
				foreach (var post in posts)
				{
					if (post.Requests.Any(x => x.EntityStatus != EntityStatus.Deleted && x.UserId == id))
					{
						requestedPosts = requestedPosts.Concat(new List<Post>(){post});
					}
				}

				//filter posts by properties
			    requestedPosts = FilterPost(request, requestedPosts);
			    requestedPosts = SortPosts(request, requestedPosts);

			    var result = requestedPosts.Skip(request.Limit * (request.Page - 1)).Take(request.Limit).Select(Mapper.Map<PostAppResponse>).ToList();
			    foreach (var post in result)
			    {
				    post.IsCurrentUserRequested = true;
			    }

				return new PagingQueryResponse<PostAppResponse>
			    {
				    Data = result,
				    PageInformation = new PageInformation
				    {
					    Total = requestedPosts.Count(),
					    Page = request.Page,
					    Limit = request.Limit
				    }
			    };
			}

		    throw new BadRequestException(CommonConstant.Error.InvalidInput);
	    }

		public T GetDetail(Guid postId, string userId)
		{
            try
            {
                var post = _postService.Include(x => x.Category).Include(y => y.Images).Include(z => z.ProvinceCity)
                    .Include(x => x.User).Include(x => x.Requests).Include(x => x.Comments).FirstOrDefault(x => x.Id == postId);
	            //just get requests that have not deleted yet
				post.Requests = post.Requests.Where(x => x.EntityStatus != EntityStatus.Deleted && x.RequestStatus == RequestStatus.Pending).ToList();

				var postResponse = Mapper.Map<T>(post);

	            if (typeof(T) == typeof(PostAppResponse) && !string.IsNullOrEmpty(userId))
	            {
		            if (Guid.TryParse(userId, out var id))
		            {
			            var postAppResponse = postResponse as PostAppResponse;

			            var requests = _requestService.FirstOrDefault(x =>
				            x.EntityStatus != EntityStatus.Deleted && x.PostId == post.Id && x.UserId == id);
			            if (requests != null)
			            {
				            postAppResponse.IsCurrentUserRequested = true;
			            }

			            return postAppResponse as T;
					}

		            throw new BadRequestException(CommonConstant.Error.InvalidInput);
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

	        throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
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
            var post = _postService.Include(x => x.Requests).FirstOrDefault(x => x.Id == postId);
	        bool updated = false;

			if (post == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

            if (request.UserStatus == PostStatus.Giving.ToString())
            {
                post.PostStatus = PostStatus.Giving;
	            updated = _postService.Update(post);
			}
            else if (request.UserStatus == PostStatus.Gave.ToString())
            {
                post.PostStatus = PostStatus.Gave;
	            updated = _postService.Update(post);
	            if (updated)
	            {
		            RejectRequests(postId);
	            }
            }
            else
                throw new BadRequestException(CommonConstant.Error.InvalidInput);

            if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

		#region Utils

	    private void RejectRequests(Guid postId)
	    {
		    var requests = _requestService.Where(x =>
			    x.PostId == postId && x.EntityStatus != EntityStatus.Deleted && x.RequestStatus == RequestStatus.Pending);
		    if (requests.Any())
		    {
			    foreach (var item in requests)
			    {
				    item.RequestStatus = RequestStatus.Rejected;
			    }

			    _requestService.UpdateMany(requests, out var isSaved);
			    if (isSaved == false)
				    throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
			}
	    }

		private void CheckIfCurrentUserRequested(string userId, List<T> posts)
		{
			if (typeof(T) == typeof(PostAppResponse) && !string.IsNullOrEmpty(userId))
			{
				if (Guid.TryParse(userId, out var id))
				{
					foreach (PostAppResponse post in posts as List<PostAppResponse>)
					{
						var requests = _requestService.FirstOrDefault(x =>
							x.EntityStatus != EntityStatus.Deleted && x.RequestStatus != RequestStatus.Rejected && x.PostId == post.Id && x.UserId == id);
						if (requests != null)
						{
							post.IsCurrentUserRequested = true;
						}
					}
				}
				else
				{
					throw new BadRequestException(CommonConstant.Error.InvalidInput);
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
	        IEnumerable<Post> posts = _postService.Include(x => x.Category)
		        .Include(x => x.Images)
		        .Include(x => x.ProvinceCity)
		        .Include(x => x.User)
		        .Include(x => x.Comments)
		        .Include(x => x.Requests)
		        .Where(x => x.Category.EntityStatus == EntityStatus.Activated);

	        if (typeof(T) == typeof(PostAppResponse))
	        {
				// Get post list for app
				posts = GetPostListForApp(userId, posts);
			}
	        else
	        {
		        // Get post list for cms
				posts = posts.Where(x => x.EntityStatus != EntityStatus.Deleted);
			}

			//filter posts by properties
			posts = FilterPost(request, posts);
			posts = SortPosts(request, posts);

			//just get requests that have not deleted yet
	        posts = posts.ToList();
			foreach (var post in posts)
			{
				post.Requests = post.Requests.Where(x => x.EntityStatus != EntityStatus.Deleted && x.RequestStatus == RequestStatus.Pending).ToList();
			}

			total = posts.Count();

			return posts
				.Skip(request.Limit * (request.Page - 1))
				.Take(request.Limit)
				.Select(Mapper.Map<T>)
				.ToList();
		}

	    private IEnumerable<Post> GetPostListForApp(string userId, IEnumerable<Post> posts)
	    {
			posts = posts.Where(x => x.EntityStatus == EntityStatus.Activated);
		    if (!string.IsNullOrEmpty(userId))
		    {
			    if (Guid.TryParse(userId, out var id))
			    {
				    posts = posts.Where(x => x.UserId == id);
			    }
			    else
			    {
				    throw new BadRequestException(CommonConstant.Error.InvalidInput);
			    }
			}
				
		    return posts;
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
