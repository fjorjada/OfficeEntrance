using EntOff.Api.Models.Entities.Users;

namespace EntOff.Api.Models.Entities.Tags
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public TagStatus Status { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
