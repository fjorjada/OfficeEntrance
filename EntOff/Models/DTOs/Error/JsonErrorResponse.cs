namespace EntOff.Api.Models.DTOs.Error
{
    public class JsonErrorResponse
    {
        public List<string> Messages { get; set; }

        public object DeveloperMessage { get; set; }
    }
}
