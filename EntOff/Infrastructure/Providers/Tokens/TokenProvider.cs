using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.Configurations.Tokens;
using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.Entities.Users;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EntOff.Api.Infrastructure.Providers.Tokens
{
    public class TokenProvider : ITokenProvider
    {
        private readonly SymmetricSecurityKey securityKey;
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IConfiguration configuration, IDistributedCache cache,
                IHttpContextAccessor httpContextAccessor)
        {
            var jwtConfiguration =
                configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();

            securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;

        }

        public string CreateToken(User user, string role, TagDto tagDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(nameof(User.InOffice), user.InOffice.ToString())
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (tagDto is not null)
            {

                claims.Add(
                    new Claim(
                        ApplicationConsts.TagName,
                        tagDto.Code));

                claims.Add(
                    new Claim(
                        ApplicationConsts.TagStatusName,
                        tagDto.Status.ToString()));

                claims.Add(
                    new Claim(
                        ApplicationConsts.TagExpirationName,
                        tagDto.ExpiresAt.ToString("dd/MM/yyyy HH:mm:ss")));

                claims.Add(
                    new Claim(
                        ApplicationConsts.TagIsAuthorizedName,
                        tagDto.IsAuthorized.ToString()));
            }

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";
        public async Task DeactivateAsync(string token)
           => await _cache.SetStringAsync(GetKey(token),
               " ", new DistributedCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow =
                       TimeSpan.FromMinutes(1)
               });
        public async Task<bool> IsActiveAsync(string token)
       => await _cache.GetStringAsync(GetKey(token)) == null;
        public async Task<bool> IsCurrentActiveToken()
       => await IsActiveAsync(GetCurrentAsync());
    }
}