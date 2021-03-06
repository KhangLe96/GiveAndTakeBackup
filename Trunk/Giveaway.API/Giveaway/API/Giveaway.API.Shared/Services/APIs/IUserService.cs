﻿using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.User;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.User;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IUserService
	{
	    User GetUser(Guid userId);
	    UserProfileResponse GetUserProfile(Guid userId);
	    PagingQueryResponse<UserProfileResponse> All(IDictionary<string, string> @params);
	    bool Update(User user);
        UserProfileResponse Update(Guid userId, UserProfileRequest request);
        LoginResponse Login(LoginRequest request);
	    UserProfileResponse SetRole(Guid userId, RoleRequest request);
	    UserProfileResponse ChangeUserStatus(Guid userId, StatusRequest request);
	    UserProfileResponse Create(CreateUserProfileRequest request);
	    LoginResponse LoginWithFacebook(FacebookConnectRequest request);
	}
}
