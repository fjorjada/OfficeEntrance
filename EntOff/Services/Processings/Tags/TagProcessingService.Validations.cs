using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Models.Exceptions.Tags;
using EntOff.Api.Models.Exceptions.Users;

namespace EntOff.Api.Services.Processings.Tags
{
    public partial class TagProcessingService
    {
        private static void ValidateStorageUser(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUserException(username);
            }
        }

        private static void ValidateStorageTag(Tag storageTag, Guid id)
        {
            if (storageTag == null)
            {
                throw new NotFoundTagException(id);
            }
        }

        private static void IsValidTagStatus(bool isValid,string value)
        {
            if (!isValid)
            {
                throw new InvalidTagException(nameof(Tag.Status), value);
            }
        }
    }
}
