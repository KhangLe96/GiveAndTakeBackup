using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using System;

namespace Giveaway.Service.Services
{
    public interface IRoleService : IEntityService<Role>
    {
        Guid GetRoleId(string roleName);
    }

    public class RoleService : EntityService<Role>, IRoleService
    {
        public Guid GetRoleId(string roleName)
        {
            var roleId = FirstOrDefault(r => r.RoleName == roleName).Id;
            if (roleId == null)
            {
                throw new BadRequestException(CommonConstant.Error.BadRequest);
            }
            return roleId;
        }
    }
}