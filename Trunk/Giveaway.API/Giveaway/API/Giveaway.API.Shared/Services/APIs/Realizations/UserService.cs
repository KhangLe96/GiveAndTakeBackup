using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using BadRequestException = Giveaway.API.Shared.Exceptions.BadRequestException;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        #region Properties

        private readonly DbService.IUserService _userService;
        private readonly DbService.IUserRoleService _userRoleService;
        private readonly DbService.IRoleService _roleService;

        #endregion

        #region Constructor

        public UserService(DbService.IUserService userService, DbService.IUserRoleService userRoleService, DbService.IRoleService roleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        #endregion

        #region public methods

        public User GetUser(Guid userId)
        {
            var user = _userService.Find(userId);
            return user;
        }

        public UserProfileResponse GetUserProfile(Guid userId)
        {
            var currentUser = GetUser(userId);
            return GenerateUserProfileResponse(currentUser);
        }

        public PagingQueryResponse<UserProfileResponse> All(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryUserRequest>();
            var users = GetPagedUsers(request);
            var pageInformation = GetPageInformation(request);
            return GeneratePagingQueryResponse(users, pageInformation);
        }

        public bool Update(User user)
        {
            var isUpdated = _userService.Update(user);
            if (!isUpdated)
            {
                throw new InternalServerErrorException(Const.Error.InternalServerError);
            }
            return true;
        }

        public UserProfileResponse Update(Guid userId, UserProfileRequest request)
        {
            var currentUser = GetUser(userId);
            var updatedUser = UpdateUserProfile(currentUser, request);
            return GenerateUserProfileResponse(updatedUser);
        }

        public UserProfileResponse SetRole(Guid userId, RoleRequest request)
        {
            var roleId = _roleService.GetRoleId(request.Role);
            _userRoleService.CreateUserRole(userId, roleId);
            return GetUserProfile(userId);
        }

        public UserProfileResponse ChangeUserStatus(Guid userId, StatusRequest request)
        {
            var updatedUser = _userService.UpdateStatus(userId, request.UserStatus);
            return GenerateUserProfileResponse(updatedUser);
        }

        public UserProfileResponse Create(CreateUserProfileRequest request)
        {
            var createdUser = _userService.Create(CreateUserFromRequest(request), out _);
            var normalUserRoleId = _roleService.FirstOrDefault(r => r.RoleName == Const.Roles.User).Id;
            _userRoleService.CreateUserRole(createdUser.Id, normalUserRoleId);
            return GetUserProfile(createdUser.Id);
        }

     

        public LoginResponse Login(LoginRequest request)
        {
            var validateResult = _userService.ValidateLogin(request);

            if (validateResult.StatusCode != HttpStatusCode.OK)
            {
                throw validateResult.ToException();
            }

            return GenerateLoginResponse(validateResult.Data as User);
        }

        #endregion

        #region private methods

        private static PagingQueryResponse<UserProfileResponse> GeneratePagingQueryResponse(List<UserProfileResponse> users, PageInformation pageInformation)
            => new PagingQueryResponse<UserProfileResponse>
            {
                Data = users,
                PageInformation = pageInformation
            };

        private PageInformation GetPageInformation(PagingQueryUserRequest request) => new PageInformation
        {
            Total = _userService.Where(u => u.EntityStatus != EntityStatus.Deleted).Count(),
            Limit = request.Limit,
            Page = request.Page
        };

        private List<UserProfileResponse> GetPagedUsers(PagingQueryUserRequest request)
        {
            var users = _userService.Where(u => u.EntityStatus != EntityStatus.Deleted);
            if (request.Name != null)
            {
                users = users.Where(u => u.FullName.Contains(request.Name));
            }
            if (request.Email != null)
            {
                users = users.Where(u => u.Email.Contains(request.Email));
            }
            if (request.PhoneNumber != null)
            {
                users = users.Where(u => u.PhoneNumber.Contains(request.PhoneNumber));
            }
            if (request.UserName != null)
            {
                users = users.Where(u => u.UserName.Contains(request.UserName));
            }
            return users
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(u => GenerateUserProfileResponse(u))
                .ToList();
        }

        private UserProfileResponse GenerateUserProfileResponse(User user) => new UserProfileResponse
        {
            Id = user.Id,
            FirstName = user.LastName,
            LastName = user.FirstName,
            Username = user.UserName,
            BirthDate = user.BirthDate,
            Gender = user.Gender.ToString(),
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Email = user.Email,
            Roles = _userRoleService.GetUserRoles(user.Id),
            AvatarUrl = user.AvatarUrl,
            Status = user.EntityStatus.ToString()
        };

        private LoginResponse GenerateLoginResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, _userRoleService.GetUserRoles(user.Id));

            var response = new LoginResponse
            {
                Profile = GenerateUserProfileResponse(user),
                RefreshToken = token.RefreshToke,
                TokenType = token.Type,
                Token = token.AccessToken,
                ExpiresIn = token.Expires
            };

            return response;
        }

        private User CreateUserFromRequest(CreateUserProfileRequest request)
        {
            var user = new User();
            UpdateUserFields(user, request);
            UpdateUserPassword(request, user);
            return user;
        }

        private User UpdateUserProfile(User user, UserProfileRequest request)
        {
            UpdateUserFields(user, request);
            Update(user);
            return user;
        }

        private void UpdateUserPassword(CreateUserProfileRequest request, User user)
        {
            var securedPassword = _userService.GenerateSecurePassword(request.Password);
            user.PasswordHash = securedPassword.Hash;
            user.PasswordSalt = securedPassword.Salt;
        }

        private static void UpdateUserFields(User user, UserProfileRequest request)
        {
            Enum.TryParse(request.Gender, out Gender gender);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.BirthDate = request.BirthDate;
            user.PhoneNumber = request.PhoneNumber;
            user.AvatarUrl = request.AvatarUrl;
            user.Address = request.Address;
            user.Gender = gender;
            user.Email = request.Email;
        }

        #endregion
    }
}
