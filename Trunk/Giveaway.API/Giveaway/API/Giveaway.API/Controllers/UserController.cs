using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    [Produces("application/json")]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <inheritdoc />
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get profile of current logged in user
        /// </summary>
        /// <returns>UserProfileResponse</returns>
        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        public UserProfileResponse GetProfile()
        {
            var userId = User.GetUserId();
            return _userService.GetUserProfile(userId);
        }

        /// <summary>
        /// Get list of user with pagination
        /// </summary>
        /// <returns>PagingQueryResponse</returns>
        [Authorize]
        [HttpGet("getList")]
        public PagingQueryResponse<UserProfileResponse> All([FromHeader]IDictionary<string, string> @params)
        {
            return _userService.All(@params);
        }

        /// <summary>
        /// Update current user's profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns>updated user profile</returns>
        [Authorize]
        [HttpPut]
        [Produces("application/json")]
        public UserProfileResponse UpdateProfile([FromBody]UserProfileRequest request)
        {
            var userId = User.GetUserId();
            return _userService.Update(userId, request);
        }

        /// <summary>
        /// login
        /// </summary>
        /// <returns>LoginResponse</returns>
        [HttpPost("login")]
        [Produces("application/json")]
        public LoginResponse Login([FromBody]LoginRequest request)
        {
            return _userService.Login(request);
        }

        /// <summary>
        /// logout 
        /// </summary>
        /// <returns>true</returns>
        [HttpGet("logout")]
        [Authorize]
        [Produces("application/json")]
        public bool Logout()
        {
            var userId = User.GetUserId();
            var user = _userService.GetUser(userId);
            if (user != null)
            {
                user.AllowTokensSince = DateTimeOffset.UtcNow;
                var isUpdated = _userService.Update(user);
                return isUpdated;
            }
            return false;
        }

        /// <summary> 
        /// Set user's role. Only available for Admin/SuperAdmin 
        /// </summary> 
        /// <returns></returns> 
        [Authorize]
        [HttpPut("role/{userId}")]
        [Produces("application/json")]
        public UserProfileResponse SetRole(Guid userId, [FromBody]RoleRequest request)
        {
            return _userService.SetRole(userId, request);
        }

        /// <summary> 
        /// Change user status. Only available for admin or super admin
        /// Available values : Activated, Blocked, Deleted
        /// </summary> 
        /// <returns>the updated user profile</returns> 
        [Authorize]
        [HttpPut("status/{userId}")]
        [Produces("application/json")]
        public UserProfileResponse ChangeUserStatus(Guid userId, [FromBody]StatusRequest request)
        {
            return _userService.ChangeUserStatus(userId, request);
        }

        /// <summary>
        /// create user
        /// </summary>
        /// <returns>user profile</returns>
        [HttpPost]
        [Produces("application/json")]
        public UserProfileResponse CreateUser([FromBody]CreateUserProfileRequest request)
        {
            return _userService.Create(request);
        }
    }
}
