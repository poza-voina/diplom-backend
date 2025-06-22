using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRouteRepository, RouteRepository>();
    }

    public static void AddMinio(this IServiceCollection services)
    {
        services.AddSingleton<IMinioClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            return new MinioClient()
                .WithEndpoint(config["Minio:Endpoint"])
                .WithCredentials(config["Minio:AccessKey"], config["Minio:SecretKey"])
                .WithSSL(false)
                .Build();
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите токен JWT в формате: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });
        });

    }

    public static void AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer") ?? throw new Exception("Issuer not found"), // Исправлено с "Issure" на "Issuer"
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.GetValue<string>("Audience") ?? throw new Exception("Audience not found"),
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey") ?? throw new Exception("Secret not found"))),
                    ValidateIssuerSigningKey = true,

                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Email
                };
            });
    }


    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<ICuePointService, CuePointService>();
        services.AddScoped<IRouteExampleService, RouteExampleService>();
        services.AddScoped<IMapService, MapService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddSingleton<IDateTimeConverter, DateTimeConverter>();
        services.AddScoped<IRouteCategoryService, RouteCategoryService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IRouteExampleRecordService, RouteExampleRecordService>();
    }

    //public static void AddApiControllers(this IServiceCollection services)
    //{
    //	var assemblies = new[]
    //	{
    //			typeof(ClientControllers.RouteActionsController).Assembly,
    //			typeof(AdminControllers.RouteController).Assembly
    //	};

    //	services.AddControllers()
    //		.ConfigureApplicationPartManager(apm =>
    //		{
    //			var added = new HashSet<string>();

    //			foreach (var assembly in assemblies)
    //			{
    //				var name = assembly.GetName().Name;

    //				if (!added.Contains(name))
    //				{
    //					apm.ApplicationParts.Add(new AssemblyPart(assembly));
    //					added.Add(name);
    //				}
    //			}
    //		});
    //}
}
