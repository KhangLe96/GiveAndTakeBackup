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
        Task<Post> GetPostDetail(string postId);
        Task<ApiPostsResponse> GetPostOfUser(string userId);
        Task ChangeStatusOfPost(string postId, string newStatus);
		Task EditPost(EditPost post);
	    Task<LoginResponse> LoginFacebook(BaseUser baseUser);
		Task<bool> CreatePost(CreatePost post, string token);
		Task<User> UpdateCurrentUserProfile(User user);
	    Task<User> GetUserProfile(string userId);
	    List<SortFilter> GetShortFilters();
	    Task<bool> CreateRequest(Request request, string token);

        Task<ApiRequestsResponse> GetRequestOfPost(string postId, string filterParams);
		Task<bool> ChangeStatusOfRequest(string requestId, string newStatus, string token);

	    Task<UserRequest> CheckUserRequest(string postId, string token);
	    Task<bool> CancelUserRequest(string postId, string token);
    }
}