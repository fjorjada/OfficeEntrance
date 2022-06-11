using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Tags
{
    public class InvalidTagException : NetXception
    {
        public InvalidTagException(string parameterName, object parameterValue)
            : base(message: $"Invalid tag, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }

        public InvalidTagException()
            : base(message: "Invalid tag. Please fix the errors and try again.") { }
    }
}
