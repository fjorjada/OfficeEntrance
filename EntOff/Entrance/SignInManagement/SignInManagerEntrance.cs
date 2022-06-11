using EntOff.Api.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Entrance.SignInManagement
{
    public class SignInManagementEntrance : ISignInManagementEntrance
    {
        private readonly SignInManager<User> signInManagement;

        public SignInManagementEntrance(SignInManager<User> signInManagement)
        {
            this.signInManagement = signInManagement;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(
            User user, 
            string password, 
            bool lockoutOnFailure)
        {
            return await this.signInManagement.CheckPasswordSignInAsync(
                user, 
                password, 
                lockoutOnFailure);
        }
    }
}
