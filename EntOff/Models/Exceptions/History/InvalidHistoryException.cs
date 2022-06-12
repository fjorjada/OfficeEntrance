
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.History

{
    public class InvalidHistoryException : NetXception
    {
        public InvalidHistoryException(string parameterName, object parameterValue)
            : base(message: $"Invalid InOut, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }

        public InvalidHistoryException()
            : base(message: "Invalid InOut. Please fix the errors and try again.") { }
    }
}
