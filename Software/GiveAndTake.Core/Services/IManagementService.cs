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
        void GetUserInformationFacebook(BaseUser baseUser);
        void GetRequest(Post post);
        void CreatePost();
        void GetNotification(User user);
        void ReportPost(Post post);
    }
}