﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.Facebook;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Avatar = Giveaway.API.Shared.Models.Avatar;
using BadRequestException = Giveaway.API.Shared.Exceptions.BadRequestException;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        #region Private Fields

        private readonly DbService.IAdminService adminService;
        private readonly DbService.ISuperAdminService superAdminService;
        private readonly DbService.IUserService userService;
        private readonly DbService.ISettingService settingService;
        private readonly IHostingEnvironment environment;


        //private readonly IFacebookService _facebookService;
        #endregion

        #region Constructor

        public AuthService(
            DbService.IAdminService adminService,
            DbService.ISuperAdminService superAdminService,
            DbService.IUserService userService,
            DbService.ISettingService settingService,
            IHostingEnvironment environment
            //IFacebookService facebookService
            )
        {
            this.adminService = adminService;
            this.superAdminService = superAdminService;
            this.userService = userService;
            this.settingService = settingService;
            //_facebookService = facebookService;
            this.environment = environment;
        }

        #endregion

        #region Methods

        public LoginResponse Login(LoginRequest request)
        {
            var validateResult = userService.ValidateLogin(request);

            if (validateResult.StatusCode != HttpStatusCode.OK)
            {
                throw validateResult.ToException();
            }

            return GenerateLoginResponse(validateResult.Data as User);
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var resultRegister = userService.ValidateRegister(request);

            if (resultRegister.StatusCode != HttpStatusCode.OK)
            {
                throw resultRegister.ToException();
            }

            var user = userService.CreateUser(request);
            //user.IsActivated = user.Role == Const.UserRoles.Student;
            user.IsActivated = true;
            var createdUser = userService.Create(user, out var isSaved);

            if (!isSaved)
            {
                throw new SystemException("Internal Error");
            }

            return GenerateRegisterResponse(createdUser);
        }

        public UserProfileResponse GetUserProfile(Guid userId)
        {
            var currentUser = userService.FirstOrDefault(x => !x.IsDeleted && x.Id == userId);

            if (currentUser == null)
            {
                throw new BadRequestException("User doesn't exist.");
            }

            return GenerateUserProfileResponse(currentUser);
        }

        public async Task<FacebookConnectResponse> LoginByFacebook(FacebookConnectRequest request)
        {
            if (string.IsNullOrEmpty(request?.FacebookId) || string.IsNullOrEmpty(request.AccessToken))
            {
                throw new BadRequestException("Empty content request");
            }
            return new FacebookConnectResponse();
        }

        public UserProfileResponse UpdateUserProfile(Guid userId, UserProfileRequest request)
        {
            var user = userService.Find(userId);

            var validateRequest = ValidateProfileRequest(user, request);

            if (validateRequest.StatusCode != HttpStatusCode.OK)
            {
                throw validateRequest.ToException();
            }

            return UpdateProfile(user, request);
        }

        public void UpdateAvatar(Guid userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new BadRequestException("Không tìm thấy file");
            }

            var dbUser = GetCurrentUser(userId);

            if (!dbUser.IsActivated)
            {
                throw new BadRequestException("Bạn chưa được kích hoạt");
            }

            var webRoot = environment.WebRootPath;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var avatar = ImageHelper.SavePhoto(userId, stream.ToArray(), webRoot, Const.AvatarFolder);
                //save avatar url to database here
            }
        }


        #endregion

        #region Private Helpers

        private User GetCurrentUser(Guid userId)
        {
            var currentUser = userService.Include(m => m.AvatarUrl).Include(m => m.Address).FirstOrDefault(x => !x.IsDeleted && x.Id == userId);

            if (currentUser == null)
            {
                throw new DataNotFoundException("Người dùng không tồn tại");
            }

            return currentUser;
        }

        private ResponseMessage ValidateProfileRequest(User user, UserProfileRequest request)
        {
            if (user == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "User doesn't exist");
            }

            if (request == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Empty request.");
            }

            //if (request.Email == null)
            //{
            //    return new ResponseMessage(HttpStatusCode.BadRequest, "Email is empty.");
            //}

            if (request.FamilyName == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "FamilyName is empty.");
            }

            if (request.GivenName == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "GivenName is empty.");
            }

            if (request.Mobilephone == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Mobiphone is empty.");
            }

            return new ResponseMessage(HttpStatusCode.OK);
        }

        private UserProfileResponse UpdateProfile(User user, UserProfileRequest request)
        {
            user.FirstName = request.GivenName;
            user.LastName = request.FamilyName;
            user.Gender = request.Gender;
            user.BirthDate = request.BirthDate;
            user.Email = request.Email;
            user.Address = request.Address;

            user.PhoneNumber = request.Mobilephone;

            var isUpdated = userService.Update(user);

            if (!isUpdated)
            {
                throw new InternalServerErrorException("couldn't update user's profile");
            }

            return GenerateUserProfileResponse(user);
        }

        private RegisterResponse GenerateRegisterResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, user.Role.ToString());

            return new RegisterResponse()
            {
                Profile = GenerateUserProfileResponse(user)
            };
        }

        private LoginResponse GenerateLoginResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, user.Role.ToString());

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

        private UserProfileResponse GenerateUserProfileResponse(User user)
        {
            var response = new UserProfileResponse()
            {
                Id = user.Id,
                FamilyName = user.LastName,
                GivenName = user.FirstName,
                Username = user.UserName,
                BirthDate = user.BirthDate,
                Gender = user.Gender.ToString(),
                Mobilephone = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                Role = user.Role.ToString(),
                Status = user.IsActivated,
                LastLogin = user.LastLogin,
                AvatarUrl = user.AvatarUrl,
                JoinedAt = user.CreatedTime
            };

            return response;
        }

        private FacebookConnectResponse GenerateFacebookConnectResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, user.Role.ToString());
            var response = new FacebookConnectResponse()
            {
                Profile = GenerateUserProfileResponse(user)
            };

            return response;
        }
        #endregion
    }
}
