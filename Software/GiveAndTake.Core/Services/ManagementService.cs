using System.Collections.Generic;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GiveAndTake.Core.Services
{
    public class ManagementService : IManagementService
    {
	    private readonly RestClient _apiHelper;

        public ManagementService()
        {
            _apiHelper = new RestClient();
        }

        public List<Category> GetCategories()
        {
            return Task.Run(async () =>
            {
                var response = await _apiHelper.Get(AppConstants.GetCategories);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    return JsonHelper.Deserialize<CategoryResponse>(response.RawContent);
                }

	            //Handle popup error cannot get data
	            Debug.WriteLine("Error cannot get data");
	            return null;
            }).Result.Categories;
        }

	    public List<ProvinceCity> GetProvinceCities()
	    {
		    throw new System.NotImplementedException();
	    }

	    public List<Post> GetPostList(string filterParams)
        {
            return Task.Run(async () =>
            {
	            var parameters = $"?{filterParams}";
				var response = await _apiHelper.Get(AppConstants.GetPostList + parameters);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    return JsonHelper.Deserialize<PostList>(response.RawContent);
                }

	            //Handle popup error cannot get data
	            Debug.WriteLine("Error cannot get data");
	            return null;

            }).Result.Posts;
        }

        public void GetPostDetail(string postId)
        {
            Task.Run(async () =>
            {
                string parameters = $"/{postId}";
                var response = await _apiHelper.Get(AppConstants.GetPostDetail + parameters);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var postDetail = JsonHelper.Deserialize<Post>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void GetPostOfUser(string userId)
        {
            Task.Run(async () =>
            {
                string parameters = $"/{userId}";
                var response = await _apiHelper.Get(AppConstants.GetPostOfUser + parameters, AppConstants.Token);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var postOfUser = JsonHelper.Deserialize<PostList>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

        public void ChangeStatusOfPost(string postId, string newStatus)  // open/close a  Post
        {
			Task.Run(async () =>
			{
				var postStatus = new PostStatus
				{
					Status = newStatus,
				};
				var statusInString = JsonHelper.Serialize(postStatus);
				var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
				string parameters = $"/{postId}";
				var response = await _apiHelper.Put(AppConstants.ChangeStatusOfPost + parameters, content, AppConstants.Token);
			});
		}

		public void EditPost(EditPost post)
		{
			Task.Run(async () =>
			{
				var postInformationInString = JsonHelper.Serialize(post);
				var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
				string parameters = $"/{post.PostId}";
				var response = await _apiHelper.Put(AppConstants.EditPost + parameters, content, AppConstants.Token);
			});
		}

		public void LoginFacebook(BaseUser baseUser)
        {
            Task.Run(async () =>
            {
                var userInformationInString = JsonHelper.Serialize(baseUser);
                var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");
                var response = await _apiHelper.Post(AppConstants.LoginFacebook, content);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var loginFacebookResponse = JsonHelper.Deserialize<LoginResponse>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

		public void CreatePost(CreatePost post)
		{
			Task.Run(async () =>
			{
				var postInformationInString = JsonHelper.Serialize(post);
				var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
				var response = await _apiHelper.Post(AppConstants.CreatePost, content, AppConstants.Token);
			});
		}

		public void UpdateCurrentUserProfile(User user)
        {
            Task.Run(async () =>
            {
                var userInformationInString = JsonHelper.Serialize(user);
                var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");
                var response = await _apiHelper.Put(AppConstants.GetUserProfile, content);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var userInformation = JsonHelper.Deserialize<User>(response.RawContent);
                }
                else
                {
                    //Handle popup error cannot get data
                    Debug.WriteLine("Error cannot get data");
                }
            });
        }

	    public void GetUserProfile(string userId)
	    {
			Task.Run(async () =>
		    {
			    string parameters = $"/{userId}";
				var response = await _apiHelper.Get(AppConstants.GetUserProfile + parameters, AppConstants.Token);
				if (response != null && response.NetworkStatus == NetworkStatus.Success)
			    {
				    var UserInformation = JsonHelper.Deserialize<User>(response.RawContent);
			    }
			    else
			    {
				    //Handle popup error cannot get data
				    Debug.WriteLine("Error cannot get data");
			    }
		    });
		}

		//public void CreateRequest(string postId);

        //public void ReportPost(string postId);

        //public void GetRequestOfPost(string postId);

		//public void GetRequestOfUser(string userId);

        //public void GetNotification(string userId);

        //public void GetCommentList(string postId);

        //public void Comment(string postId);

		//public void EditComment(string commentId);

        //public void DeleteComment(string commentId;

    }
}
