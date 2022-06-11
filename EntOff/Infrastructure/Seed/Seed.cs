using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EntOff.Api.Infrastructure.Seed
{
    public static class Seed
    {
        public static async Task SeedRoles(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            using var roleManager = serviceScope.ServiceProvider
                .GetRequiredService<RoleManager<Role>>();

            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role{Name = ApplicationConsts.AdminRoleName},
                new Role{Name = ApplicationConsts.EmployeeRoleName},
                new Role{Name = ApplicationConsts.GuestRoleName},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

        }
    }
}
