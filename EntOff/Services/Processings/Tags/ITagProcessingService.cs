using EntOff.Api.Models.DTOs.Tags;

namespace EntOff.Api.Services.Processings.Tags
{
    public interface ITagProcessingService
    {
        ValueTask<IEnumerable<TagDto>> RetrieveAllTags(string search);
        ValueTask<TagDto> UpdateUserTagAsync(string username, UpdateTagDto updateTagDto);
        ValueTask<IEnumerable<TagDto>> UpdateTagAsync();
    }
}
