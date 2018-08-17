using System.Collections.Generic;
using GiveAndTake.Core.Models;
using RestSharp;

namespace GiveAndTake.Core.Services
{
    public interface IManagementService
    {
        void GetCategories();
        void GetPostList();
        void GetPostDetail(Post post);
        void GetPostOfUser(User user);
        void ChangeStatusOfPost(Post post);
        void EditPost(Post post);
    }
}