using EntOff.Api.Infrastructure.Mappings;
using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Services.Foundations.Tags;
using EntOff.Api.Services.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace EntOff.Api.Services.Processings.Tags
{
    public partial class TagProcessingService : ITagProcessingService
    {
        private readonly ITagService tagService;
        private readonly IUserService userService;

        public TagProcessingService(ITagService tagService, IUserService userService)
        {
            this.tagService = tagService;
            this.userService = userService;
        }

        public async ValueTask<IEnumerable<TagDto>> RetrieveAllTags(string search)
        {
            var tagsDto = new List<TagDto>();

            var tags = this.tagService.RetrieveAllTags();

            bool correctStatus = Enum.TryParse(search, true, out TagStatus statusEnum);

            if (tags.Any())
            {
                var filteredTags = await tags.Where(x =>
                    string.IsNullOrWhiteSpace(search) ||
                    (correctStatus && x.Status == statusEnum))
                .ToListAsync();

                tagsDto.AddRange(filteredTags.Select(tag => tag.ToDto()));

            }

            return tagsDto;
        }

        public async ValueTask<TagDto> UpdateUserTagAsync(string username, UpdateTagDto updateTagDto)
        {
            var user = await this.userService.RetreiveUserByUserNameAsync(username);

            ValidateStorageUser(user, username);

            Tag userTag = await this.tagService.RetrieveUserTagAsync(user.Id);

            ValidateStorageTag(userTag, user.Id);

            userTag.IsAuthorized = userTag.IsAuthorized;

            if (updateTagDto.IsAuthorized is true && TagStatus.Pending == userTag.Status)
            {
                userTag.Status = TagStatus.Active;
            }

            if (userTag.ExpiresAt <= DateTime.Now)
            {
                userTag.Status = TagStatus.Expired;
            }
            else
            {
                userTag.Status = updateTagDto.Status ?? userTag.Status;
                userTag.IsAuthorized = updateTagDto.IsAuthorized;
            }

            await this.tagService.ModifyTagAsync(userTag);

            return userTag.ToDto();
        }

        public async ValueTask<IEnumerable<TagDto>> UpdateTagAsync()
        {
            var tagsDto = new List<TagDto>();

            var tags = this.tagService.RetrieveAllTags();
            if (tags.Any())
            {
                var filteredTags = await tags.ToListAsync();

                for (int i = 0; i< filteredTags.Count();i++ )
                {
                    if (filteredTags[i].ExpiresAt <= DateTime.Now)
                    {
                        filteredTags[i].Status = TagStatus.Expired;
                        await this.tagService.ModifyTagAsync(filteredTags[i]);
                    }
                }
                tagsDto.AddRange(filteredTags.Select(tag => tag.ToDto()));
               

            }

            return tagsDto;

        }
    }
}
