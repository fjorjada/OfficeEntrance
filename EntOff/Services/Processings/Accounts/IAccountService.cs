using EntOff.Api.Models.DTOs.Login;
using EntOff.Api.Models.DTOs.Register;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Models.DTOs.Logout;

namespace EntOff.Api.Services.Processings.Accounts
{
    public interface IAccountService
    {
        Task<UserDto> UserLoginAsync(LoginDto loginDto);
        Task<UserDto> UserRegisterAsync(RegisterDto registerDto);
        Task<string> UserLogoutAsync(LogoutDto logoutDto);
        
    }
}
