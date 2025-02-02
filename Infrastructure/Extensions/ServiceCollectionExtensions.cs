using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

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
		services.AddScoped<ICuePointRepository, CuePointRepository>();
	}


	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<IRouteService, RouteService>();
		services.AddScoped<ICuePointService, CuePointService>();
		services.AddScoped<IRouteExampleService, RouteExampleService>();
	}
}
