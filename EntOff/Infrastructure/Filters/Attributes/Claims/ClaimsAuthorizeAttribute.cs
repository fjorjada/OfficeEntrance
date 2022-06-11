using Microsoft.AspNetCore.Mvc;

namespace EntOff.Api.Infrastructure.Filters.Attributes.Claims
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claim)
            : base(typeof(ClaimsAuthorizationFilter))
        {
            Arguments = new object[] { claim };
        }
    }
}
