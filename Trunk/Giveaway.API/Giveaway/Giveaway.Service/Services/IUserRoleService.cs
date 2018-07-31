using System;
using System.Linq;
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
   
    public interface IUserRoleService : IEntityService<UserRole>
    {
        string[] GetUserRoles(Guid userId);
    }

    public class UserRoleService : EntityService<UserRole>, IUserRoleService
    {
        public string[] GetUserRoles(Guid userId)
        {
            return Include(ur => ur.User).Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName).ToArray();
        }
    }
}