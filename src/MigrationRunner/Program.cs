using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
	static async Task Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();

		
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		Console.WriteLine(connectionString);

		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
		optionsBuilder.UseNpgsql(connectionString);

		using var context = new ApplicationDbContext(optionsBuilder.Options);

		Console.WriteLine("Applying migrations...");
		await context.Database.MigrateAsync();

		Console.WriteLine("Migrations applied successfully.");
	}
}