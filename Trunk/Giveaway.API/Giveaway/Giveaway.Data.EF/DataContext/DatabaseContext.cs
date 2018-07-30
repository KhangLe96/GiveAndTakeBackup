using Giveaway.Data.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Giveaway.Data.EF.DataContext
{
    public class DatabaseContext : DbContext, IDataContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<User> Users { get; set; }

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
