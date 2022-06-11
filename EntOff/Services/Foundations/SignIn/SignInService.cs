using EntOff.Api.Entrance.SignInManagement;
using EntOff.Api.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Services.Foundations.SignIn
{
    public partial class SignInService : ISignInService
    {

        private readonly ISignInManagementEntrance signInManagementEntrance;

        public SignInService(ISignInManagementEntrance signInManagementEntrance)
        {
            this.signInManagementEntrance = signInManagementEntrance;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure)
        {
            var result = await this.signInManagementEntrance.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure);

            ThrowExceptionIfFailed(result);

            return result;
        }
    }
}
