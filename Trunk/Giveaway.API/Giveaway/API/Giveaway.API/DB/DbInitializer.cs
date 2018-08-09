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
        public static void Seed(IServiceProvider services)
        {
            //SeedRoles(services);
            //SeedAdmin(services);
            //SeedSuperAdmin(services);
            //SeedCategories(services);
            //SeedProvinceCity(services);
            //SeedPost(services);
            //SeedImage(services);
        }

        private static void SeedImage(IServiceProvider services)
        {
            var imageService = services.GetService<IImageService>();
            var postService = services.GetService<IPostService>();

            if (imageService.All().Any()) return;
            imageService.Create(new Image()
            {
                PostId = postService.All().Take(1).ToList().ElementAt(0).Id,
                ImageUrl = "test1",
                CreatedTime = DateTimeOffset.UtcNow,
                UpdatedTime = DateTimeOffset.Now
            }, out _);

            imageService.Create(new Image()
            {
                PostId = postService.All().Take(1).ToList().ElementAt(0).Id,
                ImageUrl = "test2",
                CreatedTime = DateTimeOffset.UtcNow,
                UpdatedTime = DateTimeOffset.Now
            }, out _);
        }

        private static void SeedProvinceCity(IServiceProvider services)
        {
            var proviceCityService = services.GetService<IProviceCityService>();
            if (proviceCityService.All().Any()) return;
            var pv = proviceCityService.Create(new ProvinceCity()
            {
                Id = Guid.NewGuid(),
                ProvinceCityName = "daklak",
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
            }, out _); 
        }

        private static void SeedPost(IServiceProvider services)
        {
            var postService = services.GetService<IPostService>();
            var proviceCityService = services.GetService<IProviceCityService>();
            var categoryService = services.GetService<ICategoryService>();
            var userService = services.GetService<IUserService>();

            if (postService.All().Any()) return;

            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
                CategoryId = categoryService.All().ToList().ElementAt(0).Id,
                Description = "Description",
                Title = "test1",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
                CategoryId = categoryService.All().Take(1).ToList().ElementAt(0).Id,
                Description = "Abvfef",
                Title = "test2",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
                CategoryId = categoryService.All().Take(1).ToList().ElementAt(0).Id,
                Description = "Abveeeeeeeeeeeeeee",
                Title = "test3",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                UpdatedTime = DateTimeOffset.UtcNow,
                CategoryId = categoryService.All().Take(1).ToList().ElementAt(0).Id,
                Description = "Abv3333333333333",
                Title = "test4",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().Take(1).ToList().ElementAt(0).Id
            }, out _);
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleService = services.GetService<IRoleService>();
            if (roleService.All().Any()) return;
            roleService.Create(new Role {RoleName = Const.Roles.User}, out _);
            roleService.Create(new Role {RoleName = Const.Roles.Admin }, out _);
            roleService.Create(new Role {RoleName = Const.Roles.SuperAdmin }, out _);
        }

        private static void SeedAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var roleService = services.GetService<IRoleService>();
            var userRoleService = services.GetService<IUserRoleService>();
            var adminRole = roleService.FirstOrDefault(r => r.RoleName == Const.Roles.Admin);
            var userRole = roleService.FirstOrDefault(r => r.RoleName == Const.Roles.User);

            if (userRoleService.Where(ur => ur.RoleId == adminRole.Id).Any()) return;
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
            if (isSaved)
            {
                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = userRole.Id }, out _);
                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = adminRole.Id }, out _);
            }
        }

        private static void SeedSuperAdmin(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var roleService = services.GetService<IRoleService>();
            var userRoleService = services.GetService<IUserRoleService>();
            var superAdminRole = roleService.FirstOrDefault(r => r.RoleName == Const.Roles.SuperAdmin);
            var userRole = roleService.FirstOrDefault(r => r.RoleName == Const.Roles.User);

            if (userRoleService.Where(ur => ur.RoleId == superAdminRole.Id).Any()) return;
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
            if (isSaved)
            {
                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = userRole.Id }, out _);
                userRoleService.Create(new UserRole { UserId = createdUser.Id, RoleId = superAdminRole.Id }, out _);
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
