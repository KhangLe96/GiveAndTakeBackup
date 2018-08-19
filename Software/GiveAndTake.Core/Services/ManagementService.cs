using System;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GiveAndTake.Core.Services
{
    public class ManagementService : IManagementService
    {
        private readonly RestClient _apiHelper;

        public ManagementService()
        {
            _apiHelper = new RestClient();
        }

        public void GetCategories()
        {
            Task.Run(async () =>
            {
                var response = await _apiHelper.Get(AppConstants.GetCategories);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var categories = JsonHelper.Deserialize<CategoryResponse>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void GetPostList()
        {
            Task.Run(async () =>
            {
                var response = await _apiHelper.Get(AppConstants.GetPostList);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var postList = JsonHelper.Deserialize<PostList>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }
        public void GetPostDetail(string postId)
        {
            Task.Run(async () =>
            {
                string parameters = $"/{postId}";
                var response = await _apiHelper.Get(AppConstants.GetPostDetail + parameters);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var PostDetail = JsonHelper.Deserialize<Post>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void GetPostOfUser(User user)
        {
            Task.Run(async () =>
            {
                string parameters = $"/{user.Id}";
                var response = await _apiHelper.Get(AppConstants.GetPostOfUser + parameters);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var PostOfUser = JsonHelper.Deserialize<PostList>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void ChangeStatusOfPost(Post post, string newStatus)  // Delete, change status of Post
        {
            Task.Run(async () =>
            {
                post.PostStatus = newStatus;
                var statusInString = JsonHelper.Serialize(post.PostStatus);
                var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
                string parameters = $"/{post.Id}";
                var response = await _apiHelper.Put(AppConstants.ChangeStatusOfPost + parameters, content);
            });
        }

        public void EditPost(Post post)
        {
            Task.Run(async () =>
            {
                var postInformationInString = JsonHelper.Serialize(post);
                var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
                string parameters = $"/{post.Id}";
                var response = await _apiHelper.Put(AppConstants.EditPost + parameters, content);
            });
        }

        public void GetNotification(User user);

        public void GetComment(Post post);

        public void DeleteComment(Post post);

        public void GetUserInformationFacebook(BaseUser baseUser)
        {
            Task.Run(async () =>
            {
                var userInformationInString = JsonHelper.Serialize(baseUser);
                var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");
                var response = await _apiHelper.Post(AppConstants.LoginFacebook, content);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var UserInformationFacebook = JsonHelper.Deserialize<LoginResponse>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void GetRequest(Post post);

        public void CreatePost();

        public void ReportPost(Post post);
    }
}
