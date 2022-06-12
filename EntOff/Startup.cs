﻿using EntOff.Api.Entrance.Storages;
using EntOff.Api.Infrastructure.Extensions.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using EntOff.Entrance.TokenManagement;
using EntOff.Api.Services.Processings.Tags;
using Hangfire;

namespace EntOff.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHangfire(x => x.UseSqlServerStorage("<connection string>"));
            //services.AddHangfireServer();
            services.AddControllerWithFilters();
            services.AddLogging();
            services.AddDbContext<StorageEntrance>();
            services.AddEntrance();
            services.AddIdentityServices(Configuration);
            services.AddTransient<TokenManagerMiddleware>();
            services.AddServices();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = Configuration["redis:connectionString"];
            });

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
            //builder.UseHangfireDashboard("/mydashboard");
            builder.UseMiddleware<TokenManagerMiddleware>();
            builder.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}
