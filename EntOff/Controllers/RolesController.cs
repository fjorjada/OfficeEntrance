using EntOff.Api.Infrastructure.Filters.Attributes.Roles;
using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.DTOs.Roles;
using EntOff.Api.Services.Foundations.Roles;
using Microsoft.AspNetCore.Mvc;

namespace EntOff.Api.Controllers
{
    [RoleAuthorize(roleName: ApplicationConsts.AdminRoleName)]

    public class RolesController : BaseApiController
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<RoleDto>> CreateRole(RoleDto roleDto) =>
            Ok(await this.roleService.AddRoleAsync(roleDto));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            return Ok(await this.roleService.RetrieveAllRoles());
        }
    }
}
