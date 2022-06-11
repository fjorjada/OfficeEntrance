using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Tags
{
    public class NullTagException : NetXception
    {
        public NullTagException() : base(message: "The tag is null.") { }
    }
}
