using EntOff.Api.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Entrance.RoleManagement
{
    public class RoleManagementEntrance : IRoleManagementEntrance
    {
        private readonly RoleManager<Role> roleManagement;

        public RoleManagementEntrance(RoleManager<Role> roleManagement)
        {
            this.roleManagement = roleManagement;
        }

        public async ValueTask<IdentityResult> InsertRoleAsync(Role role)
        {
            var ident = new RoleManagementEntrance(this.roleManagement);
            
            return await ident.roleManagement.CreateAsync(role);
        }

        public IQueryable<Role> SelectAllRoles()
        {
            var ident = new RoleManagementEntrance(this.roleManagement);

            return ident.roleManagement.Roles;
        }
    }
}
