using EntOff.Api.Models.Entities.Tags;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace EntOff.Api.Models.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool InOffice { get; set; }

        [JsonIgnore]
        public Tag Tag { get; set; }
    }
}
