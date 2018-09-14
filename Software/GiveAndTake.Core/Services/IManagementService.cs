using System.Collections.Generic;
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
		void LoginFacebook(BaseUser baseUser);
		void CreatePost(CreatePost post, string token);
		void UpdateCurrentUserProfile(User user);
	    void GetUserProfile(string userId);
	    List<SortFilter> GetShortFilters();
    }
}