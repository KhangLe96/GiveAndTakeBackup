using System;
using System.Threading.Tasks;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.DTOs.Requests;
using Microsoft.AspNetCore.Http;

namespace Giveaway.API.Shared.Services.APIs
{
	public interface IAuthService
	{
		Task<FacebookConnectResponse> LoginByFacebook(FacebookConnectRequest request);

		UserProfileResponse UpdateUserProfile(Guid userId, UserProfileRequest request);

		LoginResponse Login(LoginRequest request);

		RegisterResponse Register(RegisterRequest request);

	    void UpdateAvatar(Guid userId, IFormFile file);

        UserProfileResponse GetUserProfile(Guid userId);
	}
}
