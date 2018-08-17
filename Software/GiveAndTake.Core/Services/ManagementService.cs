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
    public struct PostStatus
    {
        public string status { get; set; }
        public PostStatus(string newStatus)
        {
            status = newStatus;
        }
    }

    public struct PostInformation
    {
        public ProvinceCity ProvinceCity { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }

        public PostInformation(ProvinceCity provinceCity, Category category, string title, string description,
            List<string> images)
        {
            ProvinceCity = provinceCity;
            Category = category;
            Title = title;
            Description = description;
            Images = images;
        }
    }
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

        public void GetPostDetail(Post post)
        {
            Task.Run(async () =>
            {
                string parameters = $"/{post.Id}";
                var response = await _apiHelper.Get(AppConstants.GetPostDetail + parameters);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var PostDetail = JsonHelper.Deserialize<Post>(response.RawContent);
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
            });
        }

        public void ChangeStatusOfPost(Post post)
        {
            Task.Run(async () =>
            {
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
    }
}
