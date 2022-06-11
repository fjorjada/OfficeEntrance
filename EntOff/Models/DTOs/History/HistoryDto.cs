using EntOff.Api.Models.Entities.Users;

namespace EntOff.Models.DTOs.History
{
    public class HistoryDto
    {
        public Boolean InOut { get; set; }
        public DateTimeOffset Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
