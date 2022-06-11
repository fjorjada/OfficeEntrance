using EntOff.Api.Models.DTOs.Roles;

namespace EntOff.Api.Services.Foundations.Roles
{
    public interface IRoleService
    {
        ValueTask<RoleDto> AddRoleAsync(RoleDto roleDto);
        ValueTask<IEnumerable<RoleDto>> RetrieveAllRoles();
    }
}
