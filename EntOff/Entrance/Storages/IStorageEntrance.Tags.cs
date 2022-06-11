using EntOff.Api.Models.Entities.Tags;
using EntOff.Models.Entities.History;

namespace EntOff.Api.Entrance.Storages
{
    public partial interface IStorageEntrance
    {
        ValueTask<Tag> InsertTagAsync(Tag tag);
        IQueryable<Tag> SelectAllTags();
        ValueTask<Tag> SelectTagByIdAsync(Guid tagId);
        ValueTask<Tag> SelectTagByUserIdAsync(Guid userId);
        ValueTask<Tag> UpdateTagAsync(Tag tag);
        ValueTask<Histo> InsertHistoryAsync(Histo tag);
        IQueryable<Histo> SelectAllHistory();

    }
}
