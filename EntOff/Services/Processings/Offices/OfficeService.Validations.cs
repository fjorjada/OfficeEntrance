using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Models.Exceptions.Users;

namespace EntOff.Api.Services.Processings.Offices
{
    public partial class OfficeService
    {
        private static void ValidateStorageUser(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUserException(username);
            }
        }
        private static void ValidateUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidUserException(nameof(User.UserName), username);
            }
        }
    }
}
