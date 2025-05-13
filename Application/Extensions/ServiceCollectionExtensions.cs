using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories;
using Core.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
		services.AddScoped<IClientService, ClientService>();
		services.AddSingleton<IPasswordManager, PasswordManager>();
		services.AddSingleton<IDateTimeConverter, DateTimeConverter>();
		services.AddScoped<IRouteCategoryService, RouteCategoryService>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IMinioService, MinioService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IBookService, BookService>();
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
