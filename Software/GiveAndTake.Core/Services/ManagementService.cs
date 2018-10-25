﻿using GiveAndTake.Core.Exceptions;
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

		public async Task<ApiRequestsResponse> GetRequestOfPost(string postId, string filterParams)
        {

			var url = string.IsNullOrEmpty(filterParams)
		        ? AppConstants.GetRequestOfPost
		        : string.Join("?", AppConstants.GetRequestOfPost, filterParams);
	        //string parameters = $"/{postId}";

	        var response = await _apiHelper.Get(url, AppConstants.Token);

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

        public async Task ChangeStatusOfPost(string postId, string newStatus)  // open/close a  Post
        {
			var postStatus = new StatusObj
			{
				Status = newStatus
			};
			var statusInString = JsonHelper.Serialize(postStatus);
			var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
			string parameters = $"/{postId}";
			var response = await _apiHelper.Put(AppConstants.ChangeStatusOfPost + parameters, content, AppConstants.Token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}
			//Task.Run(async () =>
			//{
			//	var postStatus = new StatusObj
			//	{
			//		Status = newStatus,
			//	};
			//	var statusInString = JsonHelper.Serialize(postStatus);
			//	var content = new StringContent(statusInString, Encoding.UTF8, "application/json");
			//	string parameters = $"/{postId}";
			//	var response = await _apiHelper.Put(AppConstants.ChangeStatusOfPost + parameters, content, AppConstants.Token);
			//});
		}

		public async Task EditPost(EditPost post)
		{
			var postInformationInString = JsonHelper.Serialize(post);
			var content = new StringContent(postInformationInString, Encoding.UTF8, "application/json");
			string parameters = $"/{post.PostId}";
			var response = await _apiHelper.Put(AppConstants.EditPost + parameters, content, AppConstants.Token);

			if (response.NetworkStatus != NetworkStatus.Success)
			{
				throw new AppException.ApiException(response.NetworkStatus.ToString());
			}

			if (!string.IsNullOrEmpty(response.ErrorMessage))
			{
				throw new AppException.ApiException(response.ErrorMessage);
			}
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
    }
}