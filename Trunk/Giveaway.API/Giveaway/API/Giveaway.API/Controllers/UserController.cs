using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.Models.Database;

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
        [HttpPost("logout")]
        [Authorize]
        [Produces("application/json")]
        public bool Logout()
        {
            var userId = User.GetUserId();
            var user = _userService.Find(userId);
            if (user != null)
            {
                user.AllowTokensSince = DateTimeOffset.UtcNow;
                var isUpdated = _userService.Update(user);
                return isUpdated;
            }
            return false;
        }
    }
}
