using System;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using DbService = Giveaway.Service.Services;
using Giveaway.API.Shared.Responses.User;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Handles authentication
    /// </summary>
    [Route("api/v1/passport")]
    public class AuthController : BaseController
    {
        #region Private Fields

        private readonly IAuthService _authService;
        private readonly DbService.IUserService _userService;

        #endregion

        #region Constructor

        /// <inheritdoc />
        public AuthController(
            IAuthService authService,
            DbService.IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        #endregion

        #region Action Methods

        [Authorize]
        [HttpPost("profile/me/avatar")]
        [Produces("application/json")]
        public void UpdateAvatar(IFormFile file)
        {
            _authService.UpdateAvatar(User.GetUserId(), file);
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
            return _authService.Register(request);
        }

        /// <summary>
        /// Get profile of user by her/his Id. Only available for Admin/SuperAdmin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Const.Roles.AdminOrAbove)]
        [HttpGet("profile/{userId}")]
        [Produces("application/json")]
        public UserProfileResponse GetProfileById(Guid userId)
        {
            return _authService.GetUserProfile(userId);
        }
        
        #endregion
    }
}

