namespace EntOff.Api.Models.DTOs.Users
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool InOffice { get; set; }
        public string Token { get; set; }
    }
}
