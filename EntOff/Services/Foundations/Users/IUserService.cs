using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserAsync(User user, string password);
        ValueTask<User> RetreiveUserByUserNameAsync(string username);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<string> AddToRoleAsync(User user, string roleName);
        ValueTask<string> RetreiveUserRoleAsync(User user);
        IQueryable<User> RetreiveUsersAsync();
    }
}
