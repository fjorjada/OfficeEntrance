
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.Users
{
    public class NullUserException : NetXception
    {
        public NullUserException() : base(message: "The user is null.") { }
    }
}
