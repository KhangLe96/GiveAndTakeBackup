using Giveaway.Data.EF.DTOs.Requests;
using Giveaway.Data.EF.Helpers;
using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
//using Giveaway.DataLayers.Models.IntermediateModels;

namespace Giveaway.Service.Services
{
    public interface IUserService : IEntityService<User>
    {
        ResponseMessage ValidateLogin(LoginRequest request);

        ResponseMessage ValidateRegister(RegisterRequest model);

        User CreateUser(RegisterRequest model);

        //User CreateUser(FacebookAccount model);

        (byte[] Salt, byte[] Hash) GenerateSecurePassword(string rawPassword);
    }

    public class UserService : EntityService<User>, IUserService
    {
        #region Private Fields
        private const string DefaultUserPassword = "user@123";
        private const string _usernamePattern = "[a-zA-Z0-9_]{1,20}";
        #endregion

        #region Constructor

        public UserService()
        {
        }

        #endregion

        #region Methods

        public ResponseMessage ValidateLogin(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserName))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập tên tài khoản.");
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập mật khẩu.");
            }

            var user = FirstOrDefault(x => string.Equals(x.UserName, request.UserName, StringComparison.InvariantCultureIgnoreCase));

            if (user == null)
            {
                return new ResponseMessage(HttpStatusCode.NotFound, "Tài khoản không tồn tại.");
            }

            var hash = HashHelper.ComputeHash(request.Password, user.PasswordSalt);

            if (!user.PasswordHash.SequenceEqual(hash))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Mật khẩu không chính xác.");
            }

            return new ResponseMessage(HttpStatusCode.OK, data: user);
        }

        public ResponseMessage ValidateRegister(RegisterRequest model)
        {
            if (string.IsNullOrEmpty(model.Password))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập mật khẩu.");
            }

            if (string.IsNullOrEmpty(model.Code))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập mã số.");
            }

            if (string.IsNullOrEmpty(model.PasswordRetype))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập lại mật khẩu.");
            }

            if (model.PasswordRetype != model.Password)
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Nhập lại mật khẩu không chính xác.");
            }

            if (string.IsNullOrEmpty(model.FamilyName))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập họ.");
            }

            if (string.IsNullOrEmpty(model.GivenName))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập tên.");
            }

            if (string.IsNullOrEmpty(model.Username))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng nhập tên tài khoản.");
            }

            if (string.IsNullOrEmpty(model.RegisterAs))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Vui lòng chọn vai trò.");
            }

            if (!IsUserNameFormatValid(model.Username))
            {
                return new ResponseMessage(HttpStatusCode.BadRequest, "Tên tài khoản từ 6-20 ký tự và chỉ bao gồm các ký tự A-Z & 0-9 and _.");
            }

            if (FirstOrDefault(m => m.UserName == model.Username) != null)
            {
                return new ResponseMessage(HttpStatusCode.Conflict, "Tài khoản đã tồn tại.");
            }

            return new ResponseMessage(HttpStatusCode.OK);
        }

        public User CreateUser(RegisterRequest model)
        {
            var securePassword = GenerateSecurePassword(model.Password);

            return new User()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.GivenName,
                LastName = model.FamilyName,
                PasswordSalt = securePassword.Salt,
                PasswordHash = securePassword.Hash,
                Role = model.RegisterAs,
                PhoneNumber = model.MobilePhone,
                Dob = model.Birthday,
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.Now,
                LastLogin = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
            };
        }

        //public User CreateUser(FacebookAccount model)
        //{
        //    var securePassword = GenerateSecurePassword(Const.DefaultUserPassword);

        //    return new User()
        //    {
        //        UserName = model.Email,
        //        Email = model.Email,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        PasswordSalt = securePassword.Salt,
        //        PasswordHash = securePassword.Hash,
        //        Role = Const.UserRoles.Student,
        //        Dob = model.Birthday,
        //        CreatedTime = DateTimeOffset.Now,
        //        UpdatedTime = DateTimeOffset.Now,
        //        LastLogin = DateTimeOffset.Now,
        //        IsActivated = true,
        //        Id = Guid.NewGuid(),
        //    };
        //}

        public (byte[] Salt, byte[] Hash) GenerateSecurePassword(string rawPassword)
        {
            var passwordSalt = HashHelper.GenerateSalt();
            var passwordHash = HashHelper.ComputeHash(rawPassword, passwordSalt);

            return (passwordSalt, passwordHash);
        }

        private bool IsUserNameFormatValid(string userName)
        {
            return Regex.IsMatch(userName, _usernamePattern);
        }

        private bool IsEmailFormatValid(string email)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
