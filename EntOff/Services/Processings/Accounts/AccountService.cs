using EntOff.Api.Infrastructure.Mappings;
using EntOff.Api.Infrastructure.Providers.Tokens;
using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.DTOs.Login;
using EntOff.Api.Models.DTOs.Register;
using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Models.Exceptions.Users;
using EntOff.Api.Services.Foundations.SignIn;
using EntOff.Api.Services.Foundations.Tags;
using EntOff.Api.Services.Foundations.Users;
using EntOff.Models.DTOs.Logout;

namespace EntOff.Api.Services.Processings.Accounts
{
    public partial class AccountService : IAccountService
    {
        private readonly ISignInService signInService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly ITokenProvider tokenProvider;

        public AccountService(
            ISignInService signInService,
            IUserService userService,
            ITokenProvider tokenProvider,
            ITagService tagService)
        {
            this.signInService = signInService;
            this.userService = userService;
            this.tokenProvider = tokenProvider;
            this.tagService = tagService;
        }

        public async Task<UserDto> UserLoginAsync(LoginDto loginDto)
        {
            ValidateLoginDto(loginDto);

            var user = await this.userService.RetreiveUserByUserNameAsync(
                loginDto.UserName);

            ValidateStorageUserName(user, loginDto.UserName);

            await this.signInService.CheckPasswordSignInAsync(user, loginDto.Password, false);

            var userRole = await this.userService.RetreiveUserRoleAsync(user);

            var tag = new TagDto();

            if (!string.IsNullOrWhiteSpace(userRole))
            {
                var storageTag = await this.tagService.RetrieveUserTagAsync(user.Id);
                tag = storageTag.ToDto();
            }

            string token = tokenProvider.CreateToken(user, userRole, tag);

            return user.ToDto(token);
        }

        public async Task<UserDto> UserRegisterAsync(RegisterDto registerDto)
        {
            ValidateRegisterDto(registerDto);
            var storageUser =
                await this.userService.RetreiveUserByUserNameAsync(registerDto.UserName);

            ValidateExistsStorageUser(storageUser, registerDto.UserName);
            var newUser = registerDto.ToEntity();

            var user =
                await this.userService.RegisterUserAsync(newUser, registerDto.Password);

            string userRole = !string.IsNullOrWhiteSpace(registerDto.Role)
                ? await this.userService.AddToRoleAsync(user, registerDto.Role)
                : string.Empty;

            var tag = new TagDto();

            if (!string.IsNullOrWhiteSpace(userRole))
            {
                Tag newTag = GenerateNewTag(user.Id, userRole);

                var storageTag = await this.tagService.CreateTagAsync(newTag);

                ValidateStorageTag(storageTag, user.Id);

                tag = storageTag.ToDto();
            }

            string token = tokenProvider.CreateToken(user, userRole, tag);

            return user.ToDto(token);
        }
        public async Task<string> UserLogoutAsync(LogoutDto logoutDto)
        {
            //deactivate the access token
            try {
                await tokenProvider.DeactivateCurrentAsync();
                return "logout";
            }
            catch(Exception ex)
            {
                throw new InvalidUserException();
            }
            
        }


        private static Tag GenerateNewTag(Guid userId, string userRole)
        {
            //could be added as a column in the role table
            int expirationYears = 0;

            if (userRole == ApplicationConsts.AdminRoleName)
            {
                expirationYears = 3;
            }
            else if (userRole == ApplicationConsts.EmployeeRoleName)
            {
                expirationYears = 1;
            }


            var newTag = new Tag
            {
                UserId = userId,
                Status = TagStatus.Pending,
                IsAuthorized = false,
            };
            
                newTag.ExpiresAt = expirationYears > 0
                    ? DateTime.Now.AddYears(expirationYears)
                    : DateTime.Now.AddHours(1);
            
            

            newTag.Code = $"{userId}-{Guid.NewGuid()}";

            return newTag;
        }
    }
}
