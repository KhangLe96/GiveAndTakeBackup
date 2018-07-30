using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Giveaway.Data.EF.Helpers;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    [Route("api/v1/cms")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <inheritdoc />
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = Const.UserRoles.AdminOrAbove)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public bool Delete(Guid id)
        {
            return _userService.DeleteUser(id);
        }
    }
}
