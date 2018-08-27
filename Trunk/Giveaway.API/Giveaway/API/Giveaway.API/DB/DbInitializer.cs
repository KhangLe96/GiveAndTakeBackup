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
            SeedRoles(services);
            SeedAdmin(services);
            SeedSuperAdmin(services);
            SeedCategories(services);
            SeedProvinceCity(services);

            SeedPost(services);

            SeedImage(services);
            SeedReport(services);
            SeedWarning(services);
            SeedRequest(services);
        }

        private static void SeedRequest(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var postService = services.GetService<IPostService>();
            var requestService = services.GetService<IRequestService>();

            if (!postService.All().Any() || requestService.All().Any()) return;

            Guid UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id;

            requestService.Create(new Request()
            {
                UserId = UserId,
                PostId = postService.All().Where(x => x.UserId == UserId).Take(1).ToList().ElementAt(0).Id,
                RequestMessage = "Chúng em thuộc chi đoàn Đại học Bách Khoa, hiện chi đoàn đang thực hiện chiến dịch mùa hè xanh, đến giúp đỡ các vùng gặp khó khăn nên chúng em đang kêu gọi các nhà" +
                " hảo tâm đóng góp."
            }, out _);
            requestService.Create(new Request()
            {
                UserId = UserId,
                PostId = postService.All().Where(x => x.UserId == UserId).Take(1).ToList().ElementAt(0).Id,
                RequestMessage = "Tôi thuộc tổ chức từ thiện ABC"
            }, out _);
        }

        private static void SeedWarning(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var warningMessageService = services.GetService<IWarningMessageService>();

            if (!userService.All().Any() || warningMessageService.All().Any()) return;

            Guid UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id;
            warningMessageService.Create(new WarningMessage()
            {
                UserId = UserId,
                Message = "Hình ảnh chứa nội dung nhạy cảm"
            }, out _);
            warningMessageService.Create(new WarningMessage()
            {
                UserId = UserId,
                Message = "Vui lòng không đăng tin quảng cáo"
            }, out _);
        }

        private static void SeedReport(IServiceProvider services)
        {
            var userService = services.GetService<IUserService>();
            var postService = services.GetService<IPostService>();
            var reportService = services.GetService<IReportService>();

            if (!userService.All().Any() || !postService.All().Any() || reportService.All().Any()) return;

            Guid UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id;
            reportService.Create(new Report()
            {
                UserId = UserId,
                PostId = postService.All().Where(x => x.UserId == UserId).Take(1).ToList().ElementAt(0).Id,
                Message = "Tin quảng cáo"
            }, out _);
            reportService.Create(new Report()
            {
                UserId = UserId,
                PostId = postService.All().Where(x => x.UserId == UserId).Take(1).ToList().ElementAt(0).Id,
                Message = "Đã xác nhận cho nhưng khi đến nhận không liên lạc được"
            }, out _);
        }

        private static void SeedImage(IServiceProvider services)
        {
            var imageService = services.GetService<IImageService>();
            var postService = services.GetService<IPostService>();

            if (!postService.All().Any() || imageService.All().Any()) return;
            imageService.Create(new Image()
            {
                PostId = postService.All().Take(1).ToList().ElementAt(0).Id,
                OriginalImage = "http://thongnhat.com.vn/wp-content/uploads/2017/08/219-24-%C4%91%E1%BB%8F-2.jpg",
                ResizedImage = "http://thongnhat.com.vn/wp-content/uploads/2017/08/219-24-%C4%91%E1%BB%8F-2.jpg"
            }, out _);

            imageService.Create(new Image()
            {
                PostId = postService.All().Take(1).ToList().ElementAt(0).Id,
                OriginalImage = "https://media3.scdn.vn/img2/2018/4_20/set-5-bo-quan-ao-tre-em-bo-nuoc-giai-khat-5-mau-ctks02-1m4G3-Yhmequ_simg_c052db_598x598_max.jpg",
                ResizedImage = "https://media3.scdn.vn/img2/2018/4_20/set-5-bo-quan-ao-tre-em-bo-nuoc-giai-khat-5-mau-ctks02-1m4G3-Yhmequ_simg_c052db_598x598_max.jpg"
            }, out _);
        }

        private static void SeedProvinceCity(IServiceProvider services)
        {
            var proviceCityService = services.GetService<IProviceCityService>();
            if (proviceCityService.All().Any()) return;
            var pv = proviceCityService.Create(new ProvinceCity()
            {
                Id = Guid.NewGuid(),
                ProvinceCityName = "Đà Nẵng",
            }, out _);
        }

        private static void SeedPost(IServiceProvider services)
        {
            var roleService = services.GetService<IRoleService>();
            var postService = services.GetService<IPostService>();
            var proviceCityService = services.GetService<IProviceCityService>();
            var categoryService = services.GetService<ICategoryService>();
            var userService = services.GetService<IUserService>();

            if (!roleService.All().Any() || !userService.All().Any() || !proviceCityService.All().Any() || !categoryService.All().Any() || postService.All().Any()) return;

            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(0).Id,
                Description = "Mới chuyển nhà nên có 1 vài đồ không dùng tới nữa, mọi người xem cái nào dùng được thì liên hệ mình nhé",
                Title = "Đồ nội thất cũ",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(1).Id,
                Description = "Điện thoại cũ không dùng tới, còn dùng gọi tốt, lên mạng thì hơi chậm. Mọi người quan tâm thì liên hệ mình",
                Title = "Điện thoại cũ",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(2).Id,
                Description = "Còn nhiều quần áo cho bé còn dùng tốt, bé lớn rồi nên không mặc vừa nữa. Chị em nào có trẻ nhỏ thì liên hệ nhé.",
                Title = "Quần áo cho bé",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(3).Id,
                Description = "Vỏ chai lọ, lon bia, nước ngọt sau tết còn rất nhiều. Ai đến thì mình cho luôn",
                Title = "Vỏ chai lọ, lon",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(4).Id,
                Description = "Nhà có nhiều cún con, ai thích nuôi thì liên hệ sớm nhé",
                Title = "Cho chó con",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(5).Id,
                Description = "Nhà có nhiều mèo con, ai thích nuôi thì liên hệ sớm nhé",
                Title = "Cho mèo con",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(6).Id,
                Description = "Mới chuyển nhà nên có 1 vài đồ không dùng tới nữa, mọi người xem cái nào dùng được thì liên hệ mình nhé",
                Title = "Đồ nội thất cũ",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(7).Id,
                Description = "Điện thoại cũ không dùng tới, còn dùng gọi tốt, lên mạng thì hơi chậm. Mọi người quan tâm thì liên hệ mình",
                Title = "Điện thoại cũ",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(8).Id,
                Description = "Còn nhiều quần áo cho bé còn dùng tốt, bé lớn rồi nên không mặc vừa nữa. Chị em nào có trẻ nhỏ thì liên hệ nhé.",
                Title = "Quần áo cho bé",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(0).Id,
                Description = "Vỏ chai lọ, lon bia, nước ngọt sau tết còn rất nhiều. Ai đến thì mình cho luôn",
                Title = "Vỏ chai lọ, lon",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
            postService.Create(new Post
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryService.All().ToList().ElementAt(3).Id,
                Description = "Nhà có nhiều cún con, ai thích nuôi thì liên hệ sớm nhé",
                Title = "Cho chó con",
                PostStatus = PostStatus.Open,
                ProvinceCityId = proviceCityService.All().Take(1).ToList().ElementAt(0).Id,
                UserId = userService.All().OrderBy(x => x.CreatedTime).Take(1).ToList().ElementAt(0).Id
            }, out _);
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleService = services.GetService<IRoleService>();
            if (roleService.All().Any()) return;
            roleService.Create(new Role { RoleName = Const.Roles.User }, out _);
            roleService.Create(new Role { RoleName = Const.Roles.Admin }, out _);
            roleService.Create(new Role { RoleName = Const.Roles.SuperAdmin }, out _);
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
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Không thuộc danh mục nào",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Xe cộ",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Đồ điện tử",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Mẹ và bé",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Đồ dùng cá nhân",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Nội ngoại thất, đồ gia dụng",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Thú cưng",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Văn phòng phẩm",
                }
            };
            return categories;
        }
    }
}
