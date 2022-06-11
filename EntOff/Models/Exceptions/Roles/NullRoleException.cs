using NetXceptions;

namespace EntOff.Api.Models.Exceptions.Roles
{
    public class NullRoleException : NetXception
    {
        public NullRoleException() : base(message: "The role is null.") { }
    }
}
