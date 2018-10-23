using System.Collections.Generic;
using System.Threading.Tasks;
using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.Services
{
    public interface IManagementService
    {
        List<Category> GetCategories();
	    List<ProvinceCity> GetProvinceCities();
	    ApiPostsResponse GetPostList(string filterParams);
        void GetPostDetail(string postId);
        void GetPostOfUser(string userId);
        void ChangeStatusOfPost(string postId, string newStatus);
		void EditPost(EditPost post);
		LoginResponse LoginFacebook(BaseUser baseUser);
		bool CreatePost(CreatePost post, string token);
		void UpdateCurrentUserProfile(User user);
	    void GetUserProfile(string userId);
	    List<SortFilter> GetShortFilters();
        ApiRequestsResponse GetRequestOfPost(string postId, string filterParams);
		//Task<bool> ChangeStatusOfRequest(string requestId, string newStatus, string token);
	    void ChangeStatusOfRequest(string requestId, string newStatus, string token);

    }
}