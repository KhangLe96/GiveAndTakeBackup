using GiveAndTake.Core.Models;

namespace GiveAndTake.Core.Services
{
    public interface IManagementService
    {
        void GetCategories();
        void GetPostList();
        void GetPostDetail(string postId);
        void GetPostOfUser(string userId);
        void ChangeStatusOfPost(string postId, string newStatus);
		void EditPost(EditPost post);
		void LoginFacebook(BaseUser baseUser);
		void CreatePost(CreatePost post);
		void UpdateCurrentUserProfile(User user);
	    void GetUserProfile(string userId);
    }
}