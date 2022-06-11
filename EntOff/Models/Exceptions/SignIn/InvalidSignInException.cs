using NetXceptions;

namespace EntOff.Api.Models.Exceptions.SignIn
{
    public class InvalidSignInException : NetXception
    {
        public InvalidSignInException()
            : base("Invalid password")
        {
        }

        public InvalidSignInException(string message)
            : base(message)
        {
        }
    }
}
