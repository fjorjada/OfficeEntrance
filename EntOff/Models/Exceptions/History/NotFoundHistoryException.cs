
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.History
{
    public class NotFoundHistoryException : NetXception
    {
        public NotFoundHistoryException(Guid id)
        : base(message: $"Couldn't find user with id: {id}")
        { }
    }
}
