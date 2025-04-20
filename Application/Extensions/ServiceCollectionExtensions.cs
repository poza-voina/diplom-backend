using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClientControllers = Client.Api.Controllers;
using AdminControllers = Admin.Api.Controllers;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddDbContext(this IServiceCollection services, string? connectionString)
	{
		services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
	}

	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IRouteRepository, RouteRepository>();
	}


	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<IRouteService, RouteService>();
		services.AddScoped<ICuePointService, CuePointService>();
		services.AddScoped<IRouteExampleService, RouteExampleService>();
		services.AddScoped<IMapService, MapService>();
		services.AddScoped<IUserService, UserService>();
		services.AddSingleton<IPasswordManager, PasswordManager>();
		services.AddScoped<IRouteCategoryService, RouteCategoryService>();
	}

	public static void AddApiControllers(this IServiceCollection services)
	{
		services.AddControllers()
			.ConfigureApplicationPartManager(apm =>
			{
				apm.ApplicationParts.Add(new AssemblyPart(typeof(ClientControllers.RouteController).Assembly));
				apm.ApplicationParts.Add(new AssemblyPart(typeof(ClientControllers.UserController).Assembly));

				apm.ApplicationParts.Add(new AssemblyPart(typeof(AdminControllers.RouteController).Assembly));
				apm.ApplicationParts.Add(new AssemblyPart(typeof(AdminControllers.RouteCategoryController).Assembly));
				apm.ApplicationParts.Add(new AssemblyPart(typeof(AdminControllers.AdminController).Assembly));
			});

	}
}
