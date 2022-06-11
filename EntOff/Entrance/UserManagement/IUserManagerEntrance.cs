using EntOff.Api.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Entrance.UserManagement
{
    public interface IUserManagementEntrance
    {
        ValueTask<IdentityResult> InsertUserAsync(User user, string password);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByUserNameAsync(string username);
        ValueTask<IdentityResult> UpdateUserAsync(User user);
        ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName);
        ValueTask<IList<string>> GetUserRoles(User user);
    }
}
