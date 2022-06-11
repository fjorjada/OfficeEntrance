using EntOff.Api.Entrance.RoleManagement;
using EntOff.Api.Infrastructure.Mappings;
using EntOff.Api.Models.DTOs.Roles;
using Microsoft.EntityFrameworkCore;

namespace EntOff.Api.Services.Foundations.Roles
{
    public partial class RoleService : IRoleService
    {
        private readonly IRoleManagementEntrance roleManagementEntrance;

        public RoleService(IRoleManagementEntrance roleManagementEntrance)
        {
            this.roleManagementEntrance = roleManagementEntrance;
        }

        public async ValueTask<RoleDto> AddRoleAsync(RoleDto roleDto)
        {
            ValidateRoleOnCreate(roleDto);

            var role = roleDto.ToEntity();

            var result = await roleManagementEntrance.InsertRoleAsync(role);

            ThrowExceptionIfAnyError(result);

            return roleDto;
        }

        public async ValueTask<IEnumerable<RoleDto>> RetrieveAllRoles()
        {
            var roleDtos = new List<RoleDto>();

            var roles =
                await roleManagementEntrance.SelectAllRoles().ToListAsync();

            roleDtos.AddRange(roles.Select(role => role.ToDto()));

            return roleDtos;
        }
    }
}
