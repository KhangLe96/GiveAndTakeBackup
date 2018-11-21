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
using Giveaway.Data.Models;
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

	    public PagingQueryResponse<RequestedPostResponse> GetListRequestedPostOfUser(IDictionary<string, string> @params, string userId)
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
				    .Where(x => x.Category.EntityStatus == EntityStatus.Activated &&
				                x.Requests.Any(y => y.EntityStatus != EntityStatus.Deleted && y.UserId == id));

				//filter posts by properties
			    posts = FilterPost(request, posts);
			    posts = SortPosts(request, posts);

			    var result = posts.Skip(request.Limit * (request.Page - 1))
				    .Take(request.Limit).AsEnumerable()
				    .Select(x => GenerateRequestedPostResponse(x, id))
				    .ToList();

				return new PagingQueryResponse<RequestedPostResponse>
			    {
				    Data = result,
				    PageInformation = new PageInformation
				    {
					    Total = posts.Count(),
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
            var post = _postService.Include(x => x.Images).FirstOrDefault(x => x.Id == id);
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
					UpdateImages(postRequest, oldImages);
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

		public bool ChangePostStatus(Guid postId, StatusRequest request)
        {
            var post = _postService.Include(x => x.Requests).FirstOrDefault(x => x.Id == postId);
	        bool updated = false;

			if (post == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }

	        var isStatus = Enum.TryParse(request.UserStatus, out PostStatus postStatus);
	        if (isStatus)
	        {
		        post.PostStatus = postStatus;
		        updated = _postService.Update(post);
		        if (updated && postStatus == PostStatus.Gave)
		        {
			        RejectRequests(postId);
		        }
			}
	        else
				updated = _postService.UpdateStatus(postId, request.UserStatus) != null;

			if (updated == false)
                throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);

            return updated;
        }

		#region Utils

	    private RequestedPostResponse GenerateRequestedPostResponse(Post post, Guid userId)
	    {
		    var requestedPost = Mapper.Map<RequestedPostResponse>(post);
		    if (requestedPost != null)
		    {
			    requestedPost.IsCurrentUserRequested = true;
			    var request = post.Requests.FirstOrDefault(x => x.UserId == userId && x.EntityStatus == EntityStatus.Activated);

			    requestedPost.RequestedPostStatus = request?.RequestStatus.ToString();

			    if (post.PostStatus == PostStatus.Received)
			    {
				    requestedPost.RequestedPostStatus = PostStatus.Received.ToString();
			    }
		    }
		    return requestedPost;
	    }

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

	    private void UpdateImages(PostRequest postRequest, List<Image> oldImages)
	    {
		    // get retained images from request
		    var retainedImages =
			    postRequest.Images.Where(x => x.Image.Contains("https://") || x.Image.Contains("http://")).ToList();
		    // get images needing to be deleted
		    var deletedImages = oldImages.Except(
			    // get retained images from database
			    oldImages.Where(x => retainedImages.Any(y => y.Image == x.OriginalImage))
		    ).ToList();

		    DeleteImages(deletedImages);

		    postRequest.Images = postRequest.Images.Except(retainedImages).ToList();
		    if (postRequest.Images.Count != 0) CreateImage(postRequest);
	    }

	    private void DeleteImages(List<Image> images)
	    {
		    foreach (var image in images)
		    {
			    _imageService.Delete(x => x.Id == image.Id, out var isSaved);

			    if (isSaved == false)
				    throw new InternalServerErrorException(CommonConstant.Error.InternalServerError);
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

        private List<T> GetPagedPosts(string userId, PagingQueryPostRequest request, out int total)
        {
	        var posts = _postService.Include(x => x.Category)
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
	        var postList = posts.ToList();
			foreach (var post in postList)
			{
				post.Requests = post.Requests.Where(x => x.EntityStatus != EntityStatus.Deleted && x.RequestStatus == RequestStatus.Pending).ToList();
			}

			total = postList.Count();

			return postList
				.Skip(request.Limit * (request.Page - 1))
				.Take(request.Limit)
				.Select(Mapper.Map<T>)
				.ToList();
		}

	    private IQueryable<Post> GetPostListForApp(string userId, IQueryable<Post> posts)
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
		    else
		    {
				//get only posts that are giving to show at the news feed
				posts = posts.Where(x => x.PostStatus == PostStatus.Giving);
		    }
				
		    return posts;
	    }

	    private IQueryable<Post> SortPosts(PagingQueryPostRequest request, IQueryable<Post> posts)
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

        private IQueryable<Post> FilterPost(PagingQueryPostRequest request, IQueryable<Post> posts)
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
