using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;

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

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return _userService.DeleteUser(id);
        }

        [HttpGet]
        public IEnumerable<UserProfileResponse> All()
        {
            return _userService.All();
        }
    }
}
