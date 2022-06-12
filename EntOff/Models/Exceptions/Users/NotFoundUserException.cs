
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.Users
{
    public class NotFoundUserException : NetXception
    {
        public NotFoundUserException(string username)
        : base(message: $"Couldn't find user with username: {username}.")
        { }        
    }
}
