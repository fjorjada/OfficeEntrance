using EntOff.Api.Models.DTOs.Users;
using EntOff.Models.DTOs.History;

namespace EntOff.Api.Services.Processings.Offices
{
    public interface IOfficeService
    {
        ValueTask<UserDto> EnterOfficeAsync(string username);
        ValueTask<UserDto> LeaveOfficeAsync(string username);
        ValueTask<IEnumerable<UserDto>> RetrieveAllEntries(Boolean search);
        ValueTask<IEnumerable<HistoryDto>> RetrieveAllHistory(int search);

    }
}
