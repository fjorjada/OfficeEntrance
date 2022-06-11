using EntOff.Api.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Entrance.RoleManagement
{
    public interface IRoleManagementEntrance
    {
        ValueTask<IdentityResult> InsertRoleAsync(Role role);
        IQueryable<Role> SelectAllRoles();
    }
}
