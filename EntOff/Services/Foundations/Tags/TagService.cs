using EntOff.Api.Entrance.Storages;
using EntOff.Api.Models.Entities.Tags;

namespace EntOff.Api.Services.Foundations.Tags
{
    public partial class TagService : ITagService
    {
        private readonly IStorageEntrance storageEntrance;

        public TagService(IStorageEntrance storageEntrance)
        {
            this.storageEntrance = storageEntrance;
        }

        public async ValueTask<Tag> CreateTagAsync(Tag tag)
        {
            ValidateTagOnCreate(tag);
            return await this.storageEntrance.InsertTagAsync(tag);
        }

        public IQueryable<Tag> RetrieveAllTags() =>
            this.storageEntrance.SelectAllTags();

        public async ValueTask<Tag> RetrieveUserTagAsync(Guid userId)
        {
            var storageTag = await this.storageEntrance.SelectTagByUserIdAsync(userId);
            ValidateStorageTag(storageTag, userId);

            return storageTag;
        }

        public async ValueTask<Tag> ModifyTagAsync(Tag tag)
        {
            ValidateTagOnModify(tag);
            Tag storageTag = await this.storageEntrance.SelectTagByIdAsync(tag.Id);
            ValidateStorageTag(storageTag, tag.Id);

            return await this.storageEntrance.UpdateTagAsync(tag);
        }
    }
}
