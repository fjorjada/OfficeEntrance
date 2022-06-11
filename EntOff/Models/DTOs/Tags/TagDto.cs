using EntOff.Api.Models.Entities.Tags;

namespace EntOff.Api.Models.DTOs.Tags
{
    public class TagDto
    {
        public string Code { get; set; }
        public TagStatus Status { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public Guid UserId { get; set; }
    }
}
