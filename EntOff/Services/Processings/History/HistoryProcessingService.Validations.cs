
using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Models.Exceptions.History;
using EntOff.Api.Models.Exceptions.Users;
using EntOff.Models.Entities.History;

namespace EntOff.Api.Services.Processings.History
{
    public partial class HistoryProcessingService
    {
        private static void ValidateStorageUser(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUserException(username);
            }
        }

        private static void ValidateStorageTag(Histo storageTag, Guid id)
        {
            if (storageTag == null)
            {
                throw new NotFoundHistoryException(id);
            }
        }

        private static void IsValidTagStatus(bool isValid,string value)
        {
            if (!isValid)
            {
                throw new InvalidHistoryException(nameof(Histo.Id), value);
            }
        }
    }
}
