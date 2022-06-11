using EntOff.Api.Infrastructure.Filters.Attributes.Roles;
using EntOff.Api.Models.Configurations.Authorizations;
using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Services.Processings.Tags;
using Microsoft.AspNetCore.Mvc;

namespace EntOff.Api.Controllers
{

    [RoleAuthorize(roleName: ApplicationConsts.AdminRoleName)]
    public class TagsController : BaseApiController
    {
        private readonly ITagProcessingService tagProcessingService;

        public TagsController(ITagProcessingService tagProcessingService)
        {
            this.tagProcessingService = tagProcessingService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<TagDto>>> GetTags(string? tagStatus) =>
             Ok(await this.tagProcessingService.RetrieveAllTags(tagStatus));



        [HttpPut("{username}")]
        public async ValueTask<ActionResult<TagDto>> UpdateTag(string username, UpdateTagDto updateTagDto) =>
            Ok(await this.tagProcessingService.UpdateUserTagAsync(username, updateTagDto));
    }
}
