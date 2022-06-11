using EntOff.Api.Models.Entities.Roles;
using EntOff.Api.Models.Entities.Users;
using EntOff.Models.Entities.History;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntOff.Api.Entrance.Storages
{
    public partial class StorageEntrance : IdentityDbContext<User, Role, Guid>, IStorageEntrance
    {
        private readonly IConfiguration configuration;

        public StorageEntrance(IConfiguration configuration)
        {
            this.configuration = configuration;
            //this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetIdentityTableNames(builder);
            ConfigureTagEntity(builder);
            ConfigureHistoryEntity(builder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration
                .GetConnectionString(name: "DefaultConnection");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private static void SetIdentityTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "Users");
            modelBuilder.Entity<Role>().ToTable(name: "Roles");
        }
     
    }
}
