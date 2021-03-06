﻿using Giveaway.Data.EF;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using System;
using System.Linq;

namespace Giveaway.Service.Services
{

    public interface IUserRoleService : IEntityService<UserRole>
    {
        string[] GetUserRoles(Guid userId);
        bool CreateUserRole(Guid userId, Guid roleId);
    }

    public class UserRoleService : EntityService<UserRole>, IUserRoleService
    {
        public string[] GetUserRoles(Guid userId)
        {
            return Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName).ToArray();
        }

        public bool CreateUserRole(Guid userId, Guid roleId)
        {
            if (IsUserRoleExisted(userId, roleId))
            {
                throw new BadRequestException(CommonConstant.Error.BadRequest);
            }
            Create(new UserRole { RoleId = roleId, UserId = userId }, out bool isSaved);
            if (!isSaved)
            {
                throw new InternalServerErrorException(CommonConstant.Error.BadRequest);
            }
            return true;
        }

        private bool IsUserRoleExisted(Guid userId, Guid roleId) =>
            Where(ur => ur.UserId == userId).Any(ur => ur.RoleId == roleId);
    }
}