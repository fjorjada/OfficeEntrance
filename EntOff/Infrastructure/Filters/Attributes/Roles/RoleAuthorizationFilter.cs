using EntOff.Api.Models.DTOs.Error;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Infrastructure.Filters.Attributes.Roles
{
    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string roleName;
        private readonly IHttpContextAccessor _context;
        public RoleAuthorizationFilter(string roleName, IHttpContextAccessor context)
        {
            this.roleName = roleName;
            _context = context;
        }
      

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
           
            if (user == null
                || user.Identity is null
                || !user.Identity.IsAuthenticated
                || !user.IsInRole(roleName))
            {
                var json = new JsonErrorResponse
                {
                    Messages = new List<string> { "You are not allowed to access this resource!" }
                };


                context.Result = new ForbiddenErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return;
            }
        }
    }
}
