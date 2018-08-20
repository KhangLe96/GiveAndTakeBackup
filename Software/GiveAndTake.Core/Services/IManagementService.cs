using System.Collections.Generic;
using GiveAndTake.Core.Models;
using RestSharp;

namespace GiveAndTake.Core.Services
{
    public interface IManagementService
    {
        void GetCategories();
        void GetPostList();
        void GetPostDetail(string postId);
        void GetPostOfUser(User user);
        void ChangeStatusOfPost(Post post, string newStatus);
        void EditPost(Post post);
        void LoginFacebook(BaseUser baseUser);
        //void GetRequest(Post post);
        void CreatePost(Post post);
        //void GetNotification(User user);
        //void ReportPost(Post post);
        void GetCurrentUserProfile();
    }
}