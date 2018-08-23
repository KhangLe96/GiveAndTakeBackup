using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Models;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Giveaway.Util.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Giveaway.API.Shared.Helpers
{
    public static class JwtHelper
    {
        private static readonly SymmetricSecurityKey SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.Jwt.Secret));
        private static readonly JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
        private static readonly IUserService UserService = ServiceProviderHelper.Current.GetService<IUserService>();

        public static Token CreateToken(string username, Guid id, string fullname, string[] roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Name, fullname)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var token = new JwtSecurityToken(
                Const.Jwt.Issuer,
                Const.Jwt.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow + Const.Jwt.TokenLifetime,
                new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
            );
            string refreshToken = Guid.NewGuid().ToString().Replace("-", "") + "." + id;
            Token accessToken = new Token()
            {
                AccessToken = Handler.WriteToken(token),
                Type = "bearer",
                RefreshToke = refreshToken,
                Expires = token.ValidTo
            };
            return accessToken;
        }

        public static void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = Const.Jwt.DefaultScheme;
            options.DefaultChallengeScheme = Const.Jwt.DefaultScheme;
        }

        public static void AddRefreshToken(string refreshToken)
        {

        }

        public static void ConfigureJwtBearerOptions(JwtBearerOptions options)
            => options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecretKey,
                ValidateIssuer = true,
                ValidIssuer = Const.Jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = Const.Jwt.Audience,
                ValidateLifetime = true,
                ClockSkew = Const.Jwt.TokenLifetime,
                LifetimeValidator = LifetimeValidator
            };

        public static bool LifetimeValidator(DateTime? from, DateTime? to, SecurityToken token, TokenValidationParameters options)
        {
            // If some parameters are missing, token is considered invalid
            if (!from.HasValue || !to.HasValue || token == null || options == null)
                return false;

            // If token is expired, it's considered invalid
            if (to.Value + options.ClockSkew < DateTime.UtcNow)
                return false;

            // If token is not jwt, it's considered invalid
            if (!(token is JwtSecurityToken jwtToken))
            {
                return false;
            }

            // If token doesn't contain user id, it's considered invalid
            var userId = jwtToken.Claims.GetUserId();
            var user = UserService.GetUser(userId);

            if (user == null)
            {
                return false;
            }

            // If token is issue before the allowed date, it's considered invalid
            if (from + options.ClockSkew < user.AllowTokensSince)
            {
                return false;
            }

            return true;
        }
    }
}
