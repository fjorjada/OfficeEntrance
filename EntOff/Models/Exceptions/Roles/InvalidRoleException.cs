using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Roles
{
    public class InvalidRoleException : NetXception
    {
        public InvalidRoleException()
            : base(message: "Invalid role. Please fix the errors and try again.") { }

        public InvalidRoleException(string parameterName, object parameterValue)
            : base(message: $"Invalid role, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }
    }
}
