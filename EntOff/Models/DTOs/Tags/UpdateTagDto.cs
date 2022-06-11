using EntOff.Api.Models.Entities.Tags;

namespace EntOff.Api.Models.DTOs.Tags
{
    public class UpdateTagDto
    {
        public TagStatus? Status { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
