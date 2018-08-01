using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Giveaway.Data.EF.DataContext
{
    public class DatabaseContext : DbContext, IDataContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProvinceCity> ProvinceCities { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        private readonly IConfigurationRoot _configRoot;

        public DatabaseContext(IConfigurationRoot configRoot)
        {
            _configRoot = configRoot;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configRoot.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
