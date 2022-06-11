using EntOff.Api.Infrastructure.Mappings;
using EntOff.Api.Services.Foundations.Users;
using EntOff.Services.Foundations.History;
using Microsoft.EntityFrameworkCore;

namespace EntOff.Api.Services.Processings.History
{
    public partial class HistoryProcessingService : IHistoryProcessingService
    {
        private readonly IHistoryService tagService;
        private readonly IUserService userService;

        public HistoryProcessingService(IHistoryService tagService, IUserService userService)
        {
            this.tagService = tagService;
            this.userService = userService;
        }

    }
}
