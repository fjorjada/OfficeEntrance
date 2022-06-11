using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Users
{
    public class AlreadyExistsUsernameException : NetXception
    {
        public AlreadyExistsUsernameException(string username)
            : base($"Username '{username}' is taken")
        {
        }
    }
}
