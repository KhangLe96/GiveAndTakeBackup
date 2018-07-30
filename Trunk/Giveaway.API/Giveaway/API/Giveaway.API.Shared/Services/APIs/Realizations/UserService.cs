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
        private readonly DbService.IUserService userService;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserService(DbService.IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            this.userService = userService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IQueryable<User> GetUsers()
        {
            return userService.All();
        }

        public bool UpdateUser(User user)
        {
            return userService.Update(user);
        }

        public User GetUser(Guid id)
        {
            return userService.Find(id);
        }

        public bool DeleteUser(Guid id)
        {
            userService.Delete(x => x.Id == id, out var isSaved);
            return isSaved;
        }
    }
}
