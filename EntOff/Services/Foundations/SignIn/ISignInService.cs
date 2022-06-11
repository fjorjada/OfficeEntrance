using EntOff.Api.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Services.Foundations.SignIn
{
    public interface ISignInService
    {
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    }
}
