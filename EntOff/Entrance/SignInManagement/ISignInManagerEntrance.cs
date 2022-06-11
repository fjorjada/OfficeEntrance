using EntOff.Api.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Entrance.SignInManagement
{
    public interface ISignInManagementEntrance
    {
        Task<SignInResult> CheckPasswordSignInAsync(
            User user, 
            string password, 
            bool lockoutOnFailure);
    }
}
