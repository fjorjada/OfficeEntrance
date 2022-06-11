using EntOff.Api.Services.Foundations.Tags;
using EntOff.Api.Entrance.Loggings;
using EntOff.Api.Entrance.RoleManagement;
using EntOff.Api.Entrance.SignInManagement;
using EntOff.Api.Entrance.Storages;
using EntOff.Api.Entrance.UserManagement;
using EntOff.Api.Filters.Exceptions;
using EntOff.Api.Infrastructure.Providers.Tokens;
using EntOff.Api.Models.Configurations.Tokens;
using EntOff.Api.Models.Entities.Roles;
using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Services.Foundations.Roles;
using EntOff.Api.Services.Foundations.SignIn;
using EntOff.Api.Services.Foundations.Users;
using EntOff.Api.Services.Processings.Accounts;
using EntOff.Api.Services.Processings.Offices;
using EntOff.Api.Services.Processings.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EntOff.Services.Foundations.History;

namespace EntOff.Api.Infrastructure.Extensions.Startup
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddEntrance(this IServiceCollection services)
        {
            services.AddScoped<IStorageEntrance, StorageEntrance>();
            services.AddScoped<IUserManagementEntrance, UserManagementEntrance>();
            services.AddScoped<IRoleManagementEntrance, RoleManagementEntrance>();
            services.AddScoped<ISignInManagementEntrance, SignInManagementEntrance>();
            services.AddTransient<ILoggingEntrance, LoggingEntrance>();

            return services;
        }

        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtConfiguration = configuration
               .GetSection(nameof(JwtConfiguration))
               .Get<JwtConfiguration>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<StorageEntrance>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfiguration.Key)),

                    };
                });


            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "EntOff.Api",
                    Version = "v1"
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<ITagProcessingService, TagProcessingService>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHistoryService, HistoryService>();
            

            return services;
        }

        public static IServiceCollection AddControllerWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );

            return services;
        }

    }
}
