
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.Users
{
    public class NotFoundUsernameException : NetXception
    {
        public NotFoundUsernameException(string username)
        : base(message: $"Username {username} does not exist in the system.")
        { }
    }
}
