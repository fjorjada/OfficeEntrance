using EntOff.Api.Models.Exceptions.SignIn;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Services.Foundations.SignIn
{
    public partial class SignInService
    {
        private static void ThrowExceptionIfFailed(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidSignInException();
            }

        }
    }
}
