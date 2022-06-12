using EntOff.Api.Entrance.Loggings;
using EntOff.Api.Models.DTOs.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using NetXceptions;

namespace EntOff.Api.Filters.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggingEntrance loggingEntrance;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GlobalExceptionFilter(
            ILoggingEntrance loggingEntrance,
            IWebHostEnvironment webHostEnvironment)
        {
            this.loggingEntrance = loggingEntrance;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            this.loggingEntrance.LogError(context.Exception);

            if (context.Exception is NetXception exc)
            {
                var json = new JsonErrorResponse
                {
                    Messages = new List<string>()
                };

                foreach (var item in exc.Data.Values)
                {
                    json.Messages.AddRange(item as List<string>);
                }

                if (!json.Messages.Any())
                    json.Messages.Add(exc.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new List<string> { "An error has occurred. Try again later." }
                };

                
                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}
