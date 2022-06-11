using EntOff.Api.Entrance.Storages;
using EntOff.Api.Infrastructure.Extensions.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace EntOff.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllerWithFilters();
            services.AddLogging();
            services.AddDbContext<StorageEntrance>();
            services.AddEntrance();
            services.AddIdentityServices(Configuration);
            services.AddServices();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseSwaggerService();
            builder.UseHttpsRedirection();
            builder.UseRouting();
            builder.UseCustomCors();
            builder.UseAuthentication();
            builder.UseAuthorization();
            builder.UseCors("MyPolicy");

            builder.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}
