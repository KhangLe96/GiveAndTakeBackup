using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Helpers;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.EF.Exceptions;
using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BadRequestException = Giveaway.API.Shared.Exceptions.BadRequestException;
using DbService = Giveaway.Service.Services;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    /// <inheritdoc />
    public class AuthService : IAuthService
    {
        #region Private Fields

        private readonly DbService.IUserService _userService;
        private readonly DbService.IUserRoleService _userRoleService;
        private readonly IHostingEnvironment _environment;


        //private readonly IFacebookService _facebookService;
        #endregion

        #region Constructor

        public AuthService(
            DbService.IUserService userService,
            IHostingEnvironment environment, DbService.IRoleService roleService, DbService.IUserRoleService userRoleService
            //IFacebookService facebookService
            )
        {
            _userService = userService;
            _environment = environment;
            _userRoleService = userRoleService;
        }

        #endregion

        #region Methods
        
        public RegisterResponse Register(RegisterRequest request)
        {
            var resultRegister = _userService.ValidateRegister(request);

            if (resultRegister.StatusCode != HttpStatusCode.OK)
            {
                throw resultRegister.ToException();
            }

            var user = _userService.CreateUser(request);
            //user.IsActivated = user.Role == Const.UserRoles.Student;
            user.IsActivated = true;
            var createdUser = _userService.Create(user, out var isSaved);

            if (!isSaved)
            {
                throw new SystemException("Internal Error");
            }

            return GenerateRegisterResponse(createdUser);
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
            var user = _userService.Find(userId);

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

            var webRoot = _environment.WebRootPath;
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
            var currentUser = _userService.Include(m => m.AvatarUrl).Include(m => m.Address).FirstOrDefault(x => !x.IsDeleted && x.Id == userId);

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
                return new ResponseMessage(HttpStatusCode.BadRequest, "FirstName is empty.");
            }

            if (request.GivenName == null)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "LastName is empty.");
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

            var isUpdated = _userService.Update(user);

            if (!isUpdated)
            {
                throw new InternalServerErrorException("couldn't update user's profile");
            }

            return GenerateUserProfileResponse(user);
        }

        private RegisterResponse GenerateRegisterResponse(User user)
        {
            var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, _userRoleService.GetUserRoles(user.Id));

            return new RegisterResponse()
            {
                Profile = GenerateUserProfileResponse(user)
            };
        }

        private UserProfileResponse GenerateUserProfileResponse(User user)
        {
            var response = new UserProfileResponse()
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

            return response;
        }

        //private FacebookConnectResponse GenerateFacebookConnectResponse(User user)
        //{
        //    var token = JwtHelper.CreateToken(user.UserName, user.Id, user.FullName, user.Role.ToString());
        //    var response = new FacebookConnectResponse()
        //    {
        //        Profile = GenerateUserProfileResponse(user)
        //    };

        //    return response;
        //}
        #endregion
    }
}
