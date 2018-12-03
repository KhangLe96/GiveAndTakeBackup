using System;
using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.Services
{
	public interface IManagementService
    {
	    Task<CategoryResponse> GetCategories();
	    Task<ProvinceCitiesResponse> GetProvinceCities();
	    Task<ApiPostsResponse> GetPostList(string filterParams);
	    Task<ApiPostsResponse> GetMyPostList(string id, string filterParams, string token);
        Task<Post> GetPostDetail(string postId);
        Task<ApiPostsResponse> GetPostOfUser(string userId);
        Task ChangeStatusOfPost(string postId, string newStatus, string token);
	    Task<bool> EditPost(EditPost post, string postId, string token);
		Task<LoginResponse> LoginFacebook(BaseUser baseUser);
		Task<bool> CreatePost(CreatePost post, string token);
	    Task<bool> CreateRequest(Request request, string token);
		Task<User> UpdateCurrentUserProfile(User user);
	    Task<User> GetUserProfile(string userId);
	    List<SortFilter> GetShortFilters();
        Task<ApiRequestsResponse> GetRequestOfPost(string postId, string filterParams, string token);
	    Task<Request> GetRequestById(Guid id, string token);
		Task<bool> ChangeStatusOfRequest(string requestId, string newStatus, string token);
	    Task<UserRequest> CheckUserRequest(string postId, string token);
	    Task<bool> CancelUserRequest(string postId, string token);
	    Task CreateResponse(RequestResponse requestResponse, string token);
	    Task<ApiNotificationResponse> GetNotificationList(string filterParams, string token);
	    Task<Notification> UpdateReadStatus(string notiId, bool isRead, string token);
	    Task<bool> CheckIfRequestProcessed(Guid requestId, string token);
		Task InitData();
	    Task SendPushNotificationUserInformation(PushNotificationUserInformation info, string token);

	    Task<ApiPostsResponse> GetMyRequestedPosts(string param, string token);
	    Task Logout(string token);
    }
}