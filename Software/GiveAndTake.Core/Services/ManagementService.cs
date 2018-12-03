using System;
using GiveAndTake.Core.Exceptions;
using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleJson;

namespace GiveAndTake.Core.Services
{
	public class ManagementService : IManagementService
    {
	    private readonly RestClient _apiHelper;
	    private readonly IDataModel _dataModel;

        public ManagementService(IDataModel dataModel)
        {
            _apiHelper = new RestClient();
	        _dataModel = dataModel;
        }

	    public async Task InitData()
	    {
			_dataModel.Categories = _dataModel.Categories ?? (await GetCategories()).Categories;
		    _dataModel.ProvinceCities = _dataModel.ProvinceCities ?? (await GetProvinceCities()).ProvinceCities;
			_dataModel.SortFilters = _dataModel.SortFilters ?? GetShortFilters();
		}

	    public async Task<bool> CheckIfRequestProcessed(Guid requestId, string token)
	    {
		    string parameters = $"/{requestId}";

		    var response = await _apiHelper.Get(AppConstants.CheckIfRequestProcessed + parameters, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<bool>(response.RawContent);
		}

		public async Task<Notification> UpdateReadStatus(string notiId, bool isRead, string token)
	    {
		    var status = isRead ? "{'isRead':'true'}" : "{'isRead':'false'}";
		    var content = new StringContent(status, Encoding.UTF8, "application/json");
		    string parameters = $"/{notiId}";

		    var response = await _apiHelper.Put(AppConstants.UpdateReadStatus + parameters, content, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<Notification>(response.RawContent);
		}

		public async Task<ApiNotificationResponse> GetNotificationList(string filterParams, string token)
	    {
		    var url = $"{AppConstants.GetNotificationList}";
		    url = string.Join("?", url, filterParams);

		    var response = await _apiHelper.Get(url, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<ApiNotificationResponse>(response.RawContent);
		}

		public async Task<bool> ChangeStatusOfRequest(string requestId, string newStatus, string token)
		{
			var requestStatus = new StatusObj
			{
				Status = newStatus,
			};
			var statusInString = JsonHelper.Serialize(requestStatus);
			var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
			string parameters = $"/{requestId}";
			var response = await _apiHelper.Put(AppConstants.ChangeStatusOfRequest + parameters, content, token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}

			return JsonHelper.Deserialize<bool>(response.RawContent);
		}

		public async Task<ApiRequestsResponse> GetRequestOfPost(string postId, string filterParams, string token)
		{
			var url = $"{AppConstants.GetRequestOfPost}/{postId}";
			url = string.Join("?", url, filterParams);

			var response = await _apiHelper.Get(url, token);

			if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<ApiRequestsResponse>(response.RawContent);
		}

	    public async Task<Request> GetRequestById(Guid id, string token)
	    {
		    var url = $"{AppConstants.GetRequestById}/{id}";

		    var response = await _apiHelper.Get(url, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<Request>(response.RawContent);
		}

		public async Task<CategoryResponse> GetCategories()
        {
			var response = await _apiHelper.Get(AppConstants.GetCategories);

			if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<CategoryResponse>(response.RawContent);
		}

	    public async Task<ProvinceCitiesResponse> GetProvinceCities()
	    {
		    var response = await _apiHelper.Get(AppConstants.GetProvinceCities);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<ProvinceCitiesResponse>(response.RawContent);
		}

	    public async Task<ApiPostsResponse> GetPostList(string filterParams)
        {
			var url = string.IsNullOrEmpty(filterParams)
		        ? AppConstants.GetPostList
		        : string.Join("?", AppConstants.GetPostList, filterParams);
	        var response = await _apiHelper.Get(url);

	        if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);

		}

	    public async Task<ApiPostsResponse> GetMyPostList(string id, string filterParams, string token)
	    {
		    var url = $"{AppConstants.GetMyPostList}/{id}?{filterParams}";
		    var response = await _apiHelper.Get(url, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);

	    }

	    public async Task<ApiPostsResponse> GetMyRequestedPosts(string param, string token)
	    {
			var url = $"{AppConstants.GetMyRequestedPosts}?{param}";
		    var response = await _apiHelper.Get(url, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);
		}

	    public async Task Logout(string token)
	    {
			var response = await _apiHelper.Get(AppConstants.LogoutApp, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }
	    }

	    public async Task<Post> GetPostDetail(string postId)
        {
			var parameters = $"/{postId}";
	        var response = await _apiHelper.Get(AppConstants.GetPostDetail + parameters);
	        if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<Post>(response.RawContent);
		}

        public async Task<ApiPostsResponse> GetPostOfUser(string userId)
        {
			var parameters = $"/{userId}";
	        var response = await _apiHelper.Get(AppConstants.GetPostOfUser + parameters, AppConstants.Token);

	        if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<ApiPostsResponse>(response.RawContent);
		}

	    public Task ChangeStatusOfPost(string postId, string newStatus)
	    {
		    throw new System.NotImplementedException();
	    }

	    public async Task ChangeStatusOfPost(string postId, string newStatus, string token)  // open/close a  Post
        {
			var postStatus = new StatusObj
			{
				Status = newStatus
			};
			var statusInString = JsonHelper.Serialize(postStatus);
			var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
			string parameters = $"/{postId}";
			var response = await _apiHelper.Put(AppConstants.ChangeStatusOfPost + parameters, content, token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}
		}

		public async Task<bool> EditPost(EditPost post, string postId, string token)
		{
			var postInformationInString = JsonHelper.Serialize(post);
			var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
			string parameters = $"/{postId}";
			var response = await _apiHelper.Put(AppConstants.EditPost + parameters, content, token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}

			return JsonHelper.Deserialize<bool>(response.RawContent);
		}

		public async Task<LoginResponse> LoginFacebook(BaseUser baseUser)
	    {
		    var userInformationInString = JsonHelper.Serialize(baseUser);
		    var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");

		    var response = await _apiHelper.Post(AppConstants.LoginFacebook, content);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<LoginResponse>(response.RawContent);
	    }

	    public async Task<bool> CreatePost(CreatePost post, string token)
		{
			var postInformationInString = JsonHelper.Serialize(post);
			var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
			var response = await _apiHelper.Post(AppConstants.CreatePost, content, token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}

			return JsonHelper.Deserialize<bool>(response.RawContent);
		}

		public async Task<User> UpdateCurrentUserProfile(User user)
        {
			var userInformationInString = JsonHelper.Serialize(user);
	        var content = new StringContent(userInformationInString, Encoding.UTF8, "application/json");
	        var response = await _apiHelper.Put(AppConstants.GetUserProfile, content);
	        if (response.NetworkStatus != NetworkStatus.Success)
	        {
		        throw new AppException.ApiException(response.NetworkStatus.ToString());
	        }

	        if (!string.IsNullOrEmpty(response.ErrorMessage))
	        {
		        throw new AppException.ApiException(response.ErrorMessage);
	        }

	        return JsonHelper.Deserialize<User>(response.RawContent);
		}

	    public async Task<User> GetUserProfile(string userId)
	    {
		    var parameters = $"/{userId}";
		    var response = await _apiHelper.Get(AppConstants.GetUserProfile + parameters, AppConstants.Token);
		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<User>(response.RawContent);
		}

	    public List<SortFilter> GetShortFilters() => new List<SortFilter>
	    {
			new SortFilter {FilterName = "Mới nhất (Mặc định)", FilterTag = "desc"},
			new SortFilter {FilterName = "Cũ nhất", FilterTag = "asc"}
	    };

	    public async Task<bool> CreateRequest(Request request, string token)
	    {
		    var requestInformationInString = JsonHelper.Serialize(request);
		    var content = new StringContent(requestInformationInString, Encoding.UTF8, "application/json");
		    var response = await _apiHelper.Post(AppConstants.CreateRequest, content, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<bool>(response.RawContent);
	    }

	    public async Task<UserRequest> CheckUserRequest(string postId, string token)
	    {
		    string parameters = $"/{postId}";
		    var response = await _apiHelper.Get(AppConstants.CheckUserRequest + parameters, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<UserRequest>(response.RawContent);
	    }

	    public async Task<bool> CancelUserRequest(string postId, string token)
	    {
		    var parameters = $"/{postId}";
		    var response = await _apiHelper.Delete(AppConstants.CancelUserRequest + parameters, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }

		    return JsonHelper.Deserialize<bool>(response.RawContent);
	    }

	    public async Task CreateResponse(RequestResponse requestResponse, string token)
	    {
		    var responseInformationInString = JsonHelper.Serialize(requestResponse);
		    var content = new StringContent(responseInformationInString, Encoding.UTF8, "application/json");
		    var response = await _apiHelper.Post(AppConstants.CreateResponse, content, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }
		}

	    public async Task SendPushNotificationUserInformation(PushNotificationUserInformation info, string token)
	    {
		    var responseInformationInString = JsonHelper.Serialize(info);
		    var content = new StringContent(responseInformationInString, Encoding.UTF8, "application/json");
		    var response = await _apiHelper.Post(AppConstants.RegisterPushNotificationUserInformation, content, token);

		    if (response.NetworkStatus != NetworkStatus.Success)
		    {
			    throw new AppException.ApiException(response.NetworkStatus.ToString());
		    }

		    if (!string.IsNullOrEmpty(response.ErrorMessage))
		    {
			    throw new AppException.ApiException(response.ErrorMessage);
		    }
		}
	}
}