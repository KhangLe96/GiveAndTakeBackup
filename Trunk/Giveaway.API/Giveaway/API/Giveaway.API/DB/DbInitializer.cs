using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Giveaway.Data.EF;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.DB
{
    internal static class DbInitializer
    {
        public static void Seed(IServiceProvider services)
        {
            SeedSuperAdmin(services);
            SeedAdmin(services);
            SeedCategories(services);
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var categoryService = services.GetService<ICategoryService>();
            if (!categoryService.All().Any())
            {
                categoryService.CreateMany(CategoryInit(), out var isSaved);
            }
        }

        public static List<Category> CategoryInit()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Tất cả danh mục",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "bất động sản",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Xe cộ",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "đồ điện tử",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "mẹ và bé",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Thời trang, đồ dùng cá nhân",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Nội ngoại thất, đồ gia dụng",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Giải trí, thể thao, sở thích",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Thú cưng",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Đồ dùng văn phòng, công nông nghiệp",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Dịch vụ, du lịch",
                    CreatedTime = DateTimeOffset.UtcNow,
                    UpdatedTime = DateTimeOffset.UtcNow
                }
            };
            return categories;
        }

        private static void SeedSuperAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var superAdminService = services.GetService<ISuperAdminService>();
            var settingService = services.GetService<ISettingService>();


            if (!superAdminService.All().Any())
            {
                var securePassword = userService.GenerateSecurePassword(Const.DefaultSuperAdminPassword);
                var user = new Giveaway.Data.Models.Database.User()
                {
                    UserName = "superAdmin",
                    Email = "superadmin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    PasswordSalt = securePassword.Salt,
                    PasswordHash = securePassword.Hash,
                    Role = Role.SuperAdmin,
                    PhoneNumber = "01672734732",
                    BirthDate = new DateTime(1990, 1, 1),
                    Address = String.Empty,
                    CreatedTime = DateTimeOffset.Now,
                    UpdatedTime = DateTimeOffset.Now
                };

                var createdUser = userService.Create(user, out var isSaved);

                if (isSaved)
                {
                    var superAdmin = new SuperAdmin
                    {
                        CreatedTime = DateTimeOffset.Now,
                        UpdatedTime = DateTimeOffset.Now,
                        UserId = createdUser.Id,
                    };

                    superAdminService.Create(superAdmin, out var isSave);
                }

                if (settingService.FirstOrDefault(x => x.SettingName == Const.SettingName.StartTimeOfDay) == null)
                {
                    settingService.Create(new Setting()
                    {
                        SettingName = Const.SettingName.StartTimeOfDay,
                        SettingValue = "7:00",
                        GroupType = GroupSettingType.StartTimeOfMorningLesson,
                    }, out isSaved);
                }
            }
        }

        private static void SeedAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var adminService = services.GetService<IAdminService>();

            if (!adminService.All().Any())
            {
                var securePassword = userService.GenerateSecurePassword(Const.DefaultAdminPassword);
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Address = String.Empty,
                    PasswordSalt = securePassword.Salt,
                    PasswordHash = securePassword.Hash,
                    Role = Role.Admin,
                    PhoneNumber = "01672734732",
                    BirthDate = new DateTime(1990, 1, 1),
                    CreatedTime = DateTimeOffset.Now,
                    UpdatedTime = DateTimeOffset.Now
                };

                var createdUser = userService.Create(user, out var isSaved);

                if (isSaved)
                {
                    var admin = new Admin
                    {
                        CreatedTime = DateTimeOffset.Now,
                        UpdatedTime = DateTimeOffset.Now,
                        UserId = createdUser.Id,
                    };

                    adminService.Create(admin, out var isSave);
                }
            }
        }
    }
}
