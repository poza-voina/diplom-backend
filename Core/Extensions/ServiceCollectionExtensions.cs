using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories;
using Core.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
	}


	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<IRouteService, RouteService>();
		services.AddScoped<ICuePointService, CuePointService>();
		services.AddScoped<IRouteExampleService, RouteExampleService>();
		services.AddScoped<IMapService, MapService>();
		services.AddScoped<IUserService, UserService>();
		services.AddSingleton<IPasswordManager, PasswordManager>();
	}
}
