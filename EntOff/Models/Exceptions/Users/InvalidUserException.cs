using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Users
{
    public class InvalidUserException : NetXception
    {
        public InvalidUserException(string parameterName, object parameterValue)
            : base(message: $"Invalid data, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }

        public InvalidUserException()
            : base(message: "Invalid data. Please fix the errors and try again.") { }
    }
}
