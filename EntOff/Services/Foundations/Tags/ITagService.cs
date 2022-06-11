using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.Entities.Tags;

namespace EntOff.Api.Services.Foundations.Tags
{
    public interface ITagService
    {
        ValueTask<Tag> CreateTagAsync(Tag tag);
        IQueryable<Tag> RetrieveAllTags();
        ValueTask<Tag> RetrieveUserTagAsync(Guid userId);
        ValueTask<Tag> ModifyTagAsync(Tag tag);
    }
}
