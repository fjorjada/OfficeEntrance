using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.DTOs.Error;
using EntOff.Api.Models.Entities.Tags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EntOff.Api.Infrastructure.Filters.Attributes.Claims
{
    public class ClaimsAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _claimType;

        public ClaimsAuthorizationFilter(string claimType)
        {
            _claimType = claimType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user is null
                || user.Identity is null
                || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (_claimType == ApplicationConsts.AuthorizedTagPolicy)
            {
                var claimExpirationDate =
                    user.FindFirst(c => c.Type == ApplicationConsts.TagExpirationName);

                bool hasExpired = false;
                

                if (claimExpirationDate is not null)
                {
                    _ = DateTimeOffset.TryParse(claimExpirationDate.Value, out var expirationDate);

                    if (expirationDate != default && expirationDate <= DateTimeOffset.UtcNow)
                    {
                        hasExpired = true;
                    }
                }
               

                if (!user.HasClaim(x => x.Type == ApplicationConsts.TagName) ||
                    !user.HasClaim(ApplicationConsts.TagStatusName, TagStatus.Active.ToString()) ||
                    !user.HasClaim(ApplicationConsts.TagIsAuthorizedName, "True") ||
                    hasExpired
                    )
                {

                    var json = new JsonErrorResponse
                    {
                        Messages = new List<string> { "You are not allowed to perform this action!" }
                    };


                    context.Result = new ForbiddenErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                    return;
                }
            }
        }
    }
}
