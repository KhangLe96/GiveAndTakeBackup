using System;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Handles authentication
    /// </summary>
    [Route("api/v1/passport")]
    public class AuthController : Controller
    {
        #region Private Fields

        private readonly IAuthService authService;
        private readonly DbService.IUserService userService;

        #endregion

        #region Constructor

        /// <inheritdoc />
        public AuthController(
            IAuthService authService,
            DbService.IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        #endregion

        #region Action Methods


        [HttpPost("login")]
        [Produces("application/json")]
        public LoginResponse Login([FromBody]LoginRequest request)
        {
            return authService.Login(request);
        }

        [Authorize]
        [HttpPost("profile/me/avatar")]
        [Produces("application/json")]
        public void UpdateAvatar(IFormFile file)
        {
            authService.UpdateAvatar(User.GetUserId(), file);
        }


        /// <summary>
        /// Invalidate token
        /// </summary>
        /// <returns>true</returns>
        [HttpPost("logout")]
        [Authorize]
        [Produces("application/json")]
        public bool Logout()
        {
            var userId = User.GetUserId();
            var user = userService.Find(userId);
            if (user != null)
            {
                user.AllowTokensSince = DateTimeOffset.UtcNow;
                var isUpdated = userService.Update(user);

                return isUpdated;
            }
            return false;
        }

        /// <summary>
        /// Handles Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [Produces("application/json")]
        public RegisterResponse Register([FromBody]RegisterRequest request)
        {
            return authService.Register(request);
        }

        /// <summary>
        /// Get profile of current logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("profile/me")]
        [Produces("application/json")]
        public UserProfileResponse GetProfile()
        {
            var userId = User.GetUserId();
            return authService.GetUserProfile(userId);
        }

        /// <summary>
        /// Get profile of user by her/his Id. Only available for Admin/SuperAdmin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Const.UserRoles.AdminOrAbove)]
        [HttpGet("profile/{userId}")]
        [Produces("application/json")]
        public UserProfileResponse GetProfileById(Guid userId)
        {
            return authService.GetUserProfile(userId);
        }

        /// <summary>
        /// Update current user's profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("profile/me")]
        [Produces("application/json")]
        public UserProfileResponse UpdateProfile([FromBody]UserProfileRequest request)
        {
            var userId = User.GetUserId();
            return authService.UpdateUserProfile(userId, request);
        }

        #endregion
    }
}

