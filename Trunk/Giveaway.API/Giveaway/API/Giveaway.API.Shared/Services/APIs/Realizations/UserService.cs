using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Models.Database;
using Microsoft.AspNetCore.Hosting;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly DbService.IUserService _userService;
        private readonly DbService.IUserRoleService _userRoleService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserService(DbService.IUserService userService, IHostingEnvironment hostingEnvironment, DbService.IUserRoleService userRoleService)
        {
            this._userService = userService;
            this._hostingEnvironment = hostingEnvironment;
            _userRoleService = userRoleService;
        }

        public bool UpdateUser(User user)
        {
            return _userService.Update(user);
        }

        public User GetUser(Guid id)
        {
            return _userService.Find(id);
        }

        public bool DeleteUser(Guid id)
        {
            _userService.Delete(x => x.Id == id, out var isSaved);
            return isSaved;
        }

        public List<UserProfileResponse> All()
        {
            var response = _userService.All();
            return response.Select(u => GenerateUserProfileResponse(u)).ToList();
        }

        private UserProfileResponse GenerateUserProfileResponse(User user)
        {
            return new UserProfileResponse
            {
                Id = user.Id,
                FirstName = user.LastName,
                LastName = user.FirstName,
                Username = user.UserName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                Roles = _userRoleService.GetUserRoles(user.Id),
                AvatarUrl = user.AvatarUrl
            };
        }
    }
}
