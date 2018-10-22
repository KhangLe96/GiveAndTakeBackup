using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using System;
using System.Collections.Generic;
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
		//Review ThanhVo Apply my reviews in this method for all methods which calling api
		//Review ThanhVo Check all place where call this method and catch ApiException
        public List<Category> GetCategories()
        {
            return Task.Run(async () =>
            {
                var response = await _apiHelper.Get(AppConstants.GetCategories);

				//Review ThanhVo Do we need to know network status to deserialize. You should check the result of response, the data for next step
				if (response?.NetworkStatus == NetworkStatus.Success)
	            {
		            var deserializeResult = JsonHelper.Deserialize<CategoryResponse>(response.RawContent);

		            if (deserializeResult.ArePropertiesNotNull())
		            {
			            return deserializeResult;
		            }
	            }

				//Review ThanhVo Don't add debug writeline code in the code when submiting
	            //Handle popup error cannot get data
	            Debug.WriteLine("Error cannot get data");
				//Review ThanhVo Should create our exception "ApiException" to handle disconnect internet connection cases
				//Put all code in try catch and throw ApiException if it has exception
				throw new Exception(response?.ErrorMessage);

			}).Result.Categories;
        }

	    public List<ProvinceCity> GetProvinceCities()
	    {
			return Task.Run(async () =>
			{
				var response = await _apiHelper.Get(AppConstants.GetProvinceCities);
				if (response?.NetworkStatus == NetworkStatus.Success)
				{
					var deserializeResult = JsonHelper.Deserialize<ProvinceCitiesResponse>(response.RawContent);

					if (deserializeResult.ArePropertiesNotNull())
					{
						return deserializeResult;
					}
				}

				//Handle popup error cannot get data
				Debug.WriteLine("Error cannot get data");
				throw new Exception(response?.ErrorMessage);

			}).Result.ProvinceCities;
		}

	    public ApiPostsResponse GetPostList(string filterParams)
        {
            return Task.Run(async () =>
            {
	            var url = string.IsNullOrEmpty(filterParams)
		            ? AppConstants.GetPostList
		            : string.Join("?", AppConstants.GetPostList, filterParams);
				var response = await _apiHelper.Get(url);
                if (response?.NetworkStatus == NetworkStatus.Success)
                {
                    var deserializeResult = JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);

	                if (deserializeResult.ArePropertiesNotNull())
	                {
		                return deserializeResult;
	                }
                }

	            //Handle popup error cannot get data
	            Debug.WriteLine("Error cannot get data");
				throw new Exception(response?.ErrorMessage);

			}).Result;

	       
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
                    var postOfUser = JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);
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

		public LoginResponse LoginFacebook(BaseUser baseUser)
        {
            return Task.Run(async () =>
            {
				var userInformationInString = JsonHelper.Serialize(baseUser);
				var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");

				var response = await _apiHelper.Post(AppConstants.LoginFacebook, content);
                if (response?.NetworkStatus == NetworkStatus.Success)
                {
					var loginResponse = JsonHelper.Deserialize<LoginResponse>(response.RawContent);
	                if (loginResponse.ArePropertiesNotNull())
	                {
		                return loginResponse;
	                }
                }

	            //Handle popup error cannot get data
	            Debug.WriteLine("Error cannot get data");
	            throw new Exception(response?.ErrorMessage);

            }).Result;
        }

		public bool CreatePost(CreatePost post, string token)
		{
			return Task.Run(async () =>
			{
				var postInformationInString = JsonHelper.Serialize(post);
				var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
				var response = await _apiHelper.Post(AppConstants.CreatePost, content, token);

				return response != null && response.NetworkStatus == NetworkStatus.Success;

			}).Result;
		}

		//Review ThanhVo  Check the internet connection handler branch to know how to write api method
	    public bool CreateRequest(Request request, string token)
	    {
		    return Task.Run(async () =>
		    {
			    var requestInformationInString = JsonHelper.Serialize(request);
			    var content = new StringContent(requestInformationInString, Encoding.UTF8, "application/json");
			    var response = await _apiHelper.Post(AppConstants.CreateRequest, content, token);

			    return response != null && response.NetworkStatus == NetworkStatus.Success;

		    }).Result;
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

	    public List<SortFilter> GetShortFilters() => new List<SortFilter>
	    {
			new SortFilter {FilterName = "Mới nhất (Mặc định)", FilterTag = "desc"},
			new SortFilter {FilterName = "Cũ nhất", FilterTag = "asc"}
	    };

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
