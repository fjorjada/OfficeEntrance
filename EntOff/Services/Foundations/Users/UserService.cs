using EntOff.Api.Entrance.UserManagement;
using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Services.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IUserManagementEntrance userManagementEntrance;

        public UserService(IUserManagementEntrance userManagementEntrance)
        {
            this.userManagementEntrance = userManagementEntrance;
        }

        public async ValueTask<User> RegisterUserAsync(User user, string password)
        {
            ValidateUserOnCreate(user, password);
            var result = await this.userManagementEntrance.InsertUserAsync(user, password);

            ThrowExceptionIfAnyError(result);

            return user;
        }

        public async ValueTask<User> RetreiveUserByUserNameAsync(string username) =>
            await this.userManagementEntrance.SelectUserByUserNameAsync(username.ToLower());

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            ValidateUserOnModify(user);
            User storageUser = await this.userManagementEntrance.SelectUserByUserNameAsync(user.UserName);
            ValidateStorageUser(storageUser, user.UserName);

            var result = await this.userManagementEntrance.UpdateUserAsync(user);
            ThrowExceptionIfAnyError(result);

            return user;
        }

        public async ValueTask<string> RetreiveUserRoleAsync(User user)
        {
            User storageUser = await this.userManagementEntrance.SelectUserByUserNameAsync(user.UserName);
            ValidateStorageUser(storageUser, user.UserName);

            var roles = await this.userManagementEntrance.GetUserRoles(user);

            return roles.Any() ? roles.First() : string.Empty;
        }
         public  IQueryable<User> RetreiveUsersAsync()
        {
            var storageUser = this.userManagementEntrance.SelectAllUsers();

            return storageUser;
        }

        public async ValueTask<string> AddToRoleAsync(User user, string roleName)
        {
            var result = await this.userManagementEntrance.AddToRoleAsync(user, roleName);
            ThrowExceptionIfAnyError(result);

            return roleName;
        }
    }
}
