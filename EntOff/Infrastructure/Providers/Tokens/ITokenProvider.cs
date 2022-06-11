using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Infrastructure.Providers.Tokens
{
    public interface ITokenProvider
    {
        string CreateToken(User user, string role, TagDto tagDto);
        
    }
}
