using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Giveaway.Data.EF.DataContext
{
    public class DatabaseContext : DbContext, IDataContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProvinceCity> ProvinceCities { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly IConfigurationRoot configRoot;

        public DatabaseContext(IConfigurationRoot configRoot)
        {
            this.configRoot = configRoot;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configRoot.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
