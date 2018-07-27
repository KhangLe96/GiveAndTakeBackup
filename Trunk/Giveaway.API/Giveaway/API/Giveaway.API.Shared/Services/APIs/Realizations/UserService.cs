using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Giveaway.Data.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly DbService.IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserService(DbService.IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IQueryable<User> GetUsers()
        {
            return _userService.All();
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
    }
}
