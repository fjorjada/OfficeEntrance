using EntOff.Api.Models.DTOs.Login;
using EntOff.Api.Models.DTOs.Register;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Api.Services.Processings.Accounts;
using EntOff.Models.DTOs.Logout;
using Microsoft.AspNetCore.Mvc;

namespace EntOff.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) =>
            Ok(await this.accountService.UserRegisterAsync(registerDto));

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) =>
            Ok(await this.accountService.UserLoginAsync(loginDto));

        [HttpPost("logout")]
        public async Task<ActionResult<string>> Logout(LogoutDto logoutDto) =>
             Ok(await this.accountService.UserLogoutAsync(logoutDto));
    }
}
