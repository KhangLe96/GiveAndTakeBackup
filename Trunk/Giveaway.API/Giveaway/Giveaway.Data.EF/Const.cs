using System;

namespace Giveaway.Data.EF
{
    public static class Const
    {
        public const int VerificationCodeDuration = 3; // minutes
        public const string DefaultSuperAdminUserName = "superAdmin";
        public const string DefaultSuperAdminPassword = "superadmin@123";
        public const string DefaultAdminUserName = "user123";
        public const string DefaultAdminPassword = "admin@123";
        public const string DefaultNormalUserName = "user123";
        public const string DefaultUserPassword = "user@123";
        public const string StaticFilesFolder = "Content";
        public const string UsernamePattern = "[a-zA-Z0-9_]{1,20}";

        public static class Jwt
        {
            // TODO: Consider moving to SecretManager
            // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=visual-studio
            public const string Secret = "If you are wondering what is it about, then just ignore it :)))))))))";

            public const string DefaultScheme = "JwtBearer";
            public const string Issuer = "Giveaway";
            public const string Audience = "Everyone";
            public static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(30);
            //public static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(28);
        }

        public static class Roles
        {
            public const string Admin = nameof(Admin);
            public const string SuperAdmin = nameof(SuperAdmin);
            public const string User = nameof(User);

            public const string AdminOrAbove = Admin + Separator + SuperAdmin;

            public const string Separator = ",";
        }


        public const string SmallImageSuffix = "small.jpg";
        public const string MediumImageSuffix = "medium.jpg";
        public const string BigImageSuffix = "big.jpg";
        public const string DefaultAvatar = "default-avatar.png";
        public const string AvatarFolder = "Avatars";
    }
}
