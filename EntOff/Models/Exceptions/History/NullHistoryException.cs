
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.History
{
    public class NullHistoryException : NetXception
    {
        public NullHistoryException() : base(message: "The InOut is null.") { }
    }
}
