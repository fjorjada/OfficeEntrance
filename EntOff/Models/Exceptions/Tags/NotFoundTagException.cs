
using NetXceptions;
namespace EntOff.Api.Models.Exceptions.Tags
{
    public class NotFoundTagException : NetXception
    {
        public NotFoundTagException(Guid id)
        : base(message: $"Couldn't find user with id: {id}")
        { }
    }
}
