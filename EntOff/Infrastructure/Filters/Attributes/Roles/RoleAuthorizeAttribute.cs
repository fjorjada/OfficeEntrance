using Microsoft.AspNetCore.Mvc;

namespace EntOff.Api.Infrastructure.Filters.Attributes.Roles
{
    public class RoleAuthorizeAttribute : TypeFilterAttribute
    {
        public RoleAuthorizeAttribute(string roleName) 
            : base(typeof(RoleAuthorizationFilter))
        {
            Arguments = new object[] { roleName };
        }
    }
}
