using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;
using Microsoft.AspNetCore.Hosting;
using BadRequestException = Giveaway.API.Shared.Exceptions.BadRequestException;
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
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
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

        public PagingQueryResponse<UserProfileResponse> All(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryUserRequest>();
            var users = GetPagedUsers(request);
            return new PagingQueryResponse<UserProfileResponse>
            {
                Data = users,
                Pagination = new Pagination
                {
                    Total = _userService.Count(),
                    Limit = request.Limit,
                    Page = request.Page
                }
            };
        }

        private List<UserProfileResponse> GetPagedUsers(PagingQueryUserRequest request)
        {
            var users = _userService.Where(u => !u.IsDeleted);
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

        public User Find(Guid userId)
        {
            return _userService.Find(userId);
        }

        public bool Update(User user)
        {
            return _userService.Update(user);
        }

        public UserProfileResponse GetUserProfile(Guid userId)
        {
            var currentUser = _userService.FirstOrDefault(x => !x.IsDeleted && x.Id == userId);

            if (currentUser == null)
            {
                throw new BadRequestException("User doesn't exist.");
            }

            return GenerateUserProfileResponse(currentUser);
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

        public UserProfileResponse UpdateUserProfile(Guid userId, UserProfileRequest request)
        {
            var user = _userService.Find(userId);
            return UpdateProfile(user, request);
        }

        private UserProfileResponse GenerateUserProfileResponse(User user) => new UserProfileResponse
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

        private LoginResponse GenerateLoginResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, _userRoleService.GetUserRoles(user.Id));

            var response = new LoginResponse()
            {
                Profile = GenerateUserProfileResponse(user),
                RefreshToken = token.RefreshToke,
                TokenType = token.Type,
                Token = token.AccessToken,
                ExpiresIn = token.Expires
            };

            return response;
        }
        
        private UserProfileResponse UpdateProfile(User user, UserProfileRequest request)
        {
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.BirthDate = request.BirthDate;
            user.PhoneNumber = request.PhoneNumber;
            user.AvatarUrl = request.AvatarUrl;
            user.Address = request.Address;
            user.Gender = request.Gender;
            user.Email = request.Email;

            var isUpdated = _userService.Update(user);

            if (!isUpdated)
            {
                throw new InternalServerErrorException("couldn't update user's profile");
            }

            return GenerateUserProfileResponse(user);
        }
    }
}
