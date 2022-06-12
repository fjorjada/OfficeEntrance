using EntOff.Api.Infrastructure.Filters.Attributes.Claims;
using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Api.Services.Processings.Offices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntOff.Api.Controllers
{
    [ClaimsAuthorize(claim: ApplicationConsts.AuthorizedTagPolicy)]
    public class OfficeController : BaseApiController
    {
        private readonly IOfficeService officeService;

        public OfficeController(IOfficeService officeService)
        {
            this.officeService = officeService;
        }

        [HttpPost("enter")]
        public async ValueTask<ActionResult<UserDto>> EnterOffice() =>
            Ok(await this.officeService.EnterOfficeAsync(HttpContext.User.FindFirst(ClaimTypes.Email).Value));

        [HttpPost("leave")]
        public async ValueTask<ActionResult<UserDto>> LeaveOffice() =>
            Ok(await this.officeService.LeaveOfficeAsync(HttpContext.User.FindFirst(ClaimTypes.Email).Value));

        [HttpGet("GetAllEntries")]
        public async ValueTask<ActionResult<IEnumerable<UserDto>>> GetAllEntries(Boolean search) =>
           Ok(await this.officeService.RetrieveAllEntries(search));

        [HttpGet("GetHistory")]
        public async ValueTask<ActionResult<IEnumerable<UserDto>>> GetHistory(int search) =>
           Ok(await this.officeService.RetrieveAllHistory(search));
    }
}
