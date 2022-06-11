using EntOff.Api.Infrastructure.Mappings;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Services.Foundations.Users;
using EntOff.Models.DTOs.History;
using EntOff.Models.Entities.History;
using EntOff.Services.Foundations.History;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntOff.Api.Services.Processings.Offices
{
    public partial class OfficeService : IOfficeService
    {
        private readonly IUserService userService;
        private readonly IHistoryService historyService;
        public OfficeService(IUserService userService, IHistoryService historyService)
        {
            this.userService = userService;
            this.historyService = historyService;
        }

        
        public async ValueTask<UserDto> EnterOfficeAsync(string username)
        {
            ValidateUserName(username);
            var user = await this.userService.RetreiveUserByUserNameAsync(username);
            ValidateStorageUser(user, username);
            Histo his = new Histo();
            his.UserId = user.Id;
            his.InOut = true;
            his.Date = DateTime.Now;
            var history =await this.historyService.CreateHistoryAsync(his);

            user.InOffice = true;
            await this.userService.UpdateUserAsync(user);

            return user.ToDto();
        }

        public async ValueTask<UserDto> LeaveOfficeAsync(string username)
        {
            ValidateUserName(username);
            var user = await this.userService.RetreiveUserByUserNameAsync(username);
            ValidateStorageUser(user, username);
            Histo his = new Histo();
            his.UserId = user.Id;
            his.InOut = false;
            his.Date = DateTime.Now;
            var history = await this.historyService.CreateHistoryAsync(his);
            user.InOffice = false;
            await this.userService.UpdateUserAsync(user);

            return user.ToDto();
        }

        public async ValueTask<IEnumerable<UserDto>> RetrieveAllEntries(Boolean search)
        {
            var userentries = new List<UserDto>();
            var tags = this.userService.RetreiveUsersAsync();
            if (tags != null)
            {
                var filtered = await tags.Where(x => x.InOffice == search).ToListAsync();
                userentries.AddRange(filtered.Select(y => y.ToDto()));
            }
           
            return userentries;
        }
        public async ValueTask<IEnumerable<HistoryDto>> RetrieveAllHistory(int search)
        {
            var history = new List<HistoryDto>();
            var histo = this.historyService.GetHistoryAsync();
            var tags = this.userService.RetreiveUsersAsync();
            Nullable<bool> searc = null;
            if (histo != null)
            {
                switch (search)
            {
                case 1:  searc = false;
                    break;
                case 2: searc = true;
                    break;
                case 3:
                         searc = null;
                        break;
            }
               
                var filtered = await histo.Where(x => searc == null ? search==3 : x.InOut == searc).ToListAsync();

                history.AddRange(filtered.Select(y => y.HisDto()));
            }
            return history;
            
        }
    }
}
