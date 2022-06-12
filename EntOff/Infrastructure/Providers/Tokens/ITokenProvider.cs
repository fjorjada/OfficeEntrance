using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Infrastructure.Providers.Tokens
{
    public interface ITokenProvider
    {
        string CreateToken(User user, string role, TagDto tagDto);
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
        Task<bool> IsCurrentActiveToken();

    }
}
