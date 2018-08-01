using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Giveaway.Data.EF;
using Giveaway.Data.Models.Database;
using Giveaway.Data.Enums;

namespace Giveaway.API.DB
{
    internal static class DbInitializer
    {
        private static Role normalUserRole;
        private static Role adminRole;
        private static Role superAdminRole;

        public static void Seed(IServiceProvider services)
        {
            SeedRoles(services);
            SeedAdmin(services);
            SeedSuperAdmin(services);
            SeedCategories(services);
            SeedProvinceCity(services);
            SeedPost(services);
        }

        //private static void SeedUser(IServiceProvider services)
        //{
        //    var proviceCityService = services.GetService<IProviceCityService>();
        //    if (proviceCityService.All().Any()) return;
        //    var pv = proviceCityService.Create(new ProvinceCity() { ProvinceCityName = "daklak" }, out _);
        //}

        private static void SeedProvinceCity(IServiceProvider services)
        {
            var proviceCityService = services.GetService<IProviceCityService>();
            if (proviceCityService.All().Any()) return;
            var pv = proviceCityService.Create(new ProvinceCity(){ProvinceCityName = "daklak"}, out _);
        }

        private static void SeedPost(IServiceProvider services)
        {
            var postService = services.GetService<IPostService>();
            if (postService.All().Any()) return;

            var post = postService.Create(new Post
                {
                    Id = Guid.NewGuid(),
                    CreatedTime = DateTimeOffset.Now,
                    UpdatedTime = DateTimeOffset.UtcNow,
                    CategoryId = Guid.Parse("0016cba7-57d5-4f7c-b380-8ce5a87208c2"),
                    Description = "Description",
                    Title = "test",
                    PostStatus = PostStatus.Open,
                    ProvinceCityId = Guid.Parse("cdbe4d14-c060-486b-b1ac-0f681ec1620e"),
                    UserId = Guid.Parse("28698ed3-12ff-407a-bc6b-a0af6f386f52"),
                }, out _);
            var post1 = postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
                CategoryId = Guid.Parse("0016cba7-57d5-4f7c-b380-8ce5a87208c2"),
                Description = "Abv",
                Title = "test",
                PostStatus = PostStatus.Open,
                ProvinceCityId = Guid.Parse("cdbe4d14-c060-486b-b1ac-0f681ec1620e"),
                UserId = Guid.Parse("28698ed3-12ff-407a-bc6b-a0af6f386f52"),
            }, out _);
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleService = services.GetService<IRoleService>();
            if (roleService.All().Any()) return;
            normalUserRole = roleService.Create(new Role {UpdatedTime = DateTimeOffset.UtcNow, RoleName = "User"}, out _);
            adminRole = roleService.Create(new Role {UpdatedTime = DateTimeOffset.UtcNow, RoleName = "Admin"}, out _);
            superAdminRole = roleService.Create(new Role {UpdatedTime = DateTimeOffset.UtcNow, RoleName = "SuperAdmin"}, out _);
        }

        private static void SeedAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var adminService = services.GetService<IAdminService>();
            var userRoleService = services.GetService<IUserRoleService>();

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
                    PhoneNumber = "01672734732",
                    BirthDate = new DateTime(1990, 1, 1)
                };

                var createdUser = userService.Create(user, out var isSaved);

                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = normalUserRole.Id }, out _);

                if (isSaved)
                {
                    var admin = new Admin
                    {
                        CreatedTime = DateTimeOffset.Now,
                        UpdatedTime = DateTimeOffset.Now,
                        UserId = createdUser.Id,
                    };

                    adminService.Create(admin, out _);

                    userRoleService.Create(new UserRole {UserId = createdUser.Id, RoleId = adminRole.Id}, out _);
                }

             
            }
        }

        private static void SeedSuperAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var superAdminService = services.GetService<ISuperAdminService>();
            var userRoleService = services.GetService<IUserRoleService>();

            if (!superAdminService.All().Any())
            {
                var securePassword = userService.GenerateSecurePassword(Const.DefaultSuperAdminPassword);
                var user = new User
                {
                    UserName = "superAdmin",
                    Email = "superadmin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    PasswordSalt = securePassword.Salt,
                    PasswordHash = securePassword.Hash,
                    PhoneNumber = "01672734732",
                    BirthDate = new DateTime(1990, 1, 1),
                    Address = string.Empty
                };

                var createdUser = userService.Create(user, out var isSaved);

                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = normalUserRole.Id }, out _);

                if (isSaved)
                {
                    var superAdmin = new SuperAdmin {UserId = createdUser.Id};

                    superAdminService.Create(superAdmin, out _);

                    userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = superAdminRole.Id }, out _);
                }
            }
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var categoryService = services.GetService<ICategoryService>();
            if (!categoryService.All().Any())
            {
                categoryService.CreateMany(CategoryInit(), out _);
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
    }
}
