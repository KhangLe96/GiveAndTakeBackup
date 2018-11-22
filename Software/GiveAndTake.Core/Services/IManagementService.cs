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
        Task ChangeStatusOfPost(string postId, string newStatus);
		Task EditPost(EditPost post);
	    Task<LoginResponse> LoginFacebook(BaseUser baseUser);
		Task<bool> CreatePost(CreatePost post, string token);
		Task<User> UpdateCurrentUserProfile(User user);
	    Task<User> GetUserProfile(string userId);
	    List<SortFilter> GetShortFilters();
	    Task<ApiPostsResponse> GetMyRequestedPosts(string param, string token);
	    Task Logout(string token);
    }
}