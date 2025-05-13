using System.Text;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
	public DbSet<Route> Routes => Set<Route>();
	public DbSet<CuePoint> CuePoints => Set<CuePoint>();
	public DbSet<RouteExample> RouteExample => Set<RouteExample>();
	public DbSet<Client> Clients => Set<Client>();
	public DbSet<Admin> Admins => Set<Admin>();
	public DbSet<RouteCategory> RouteCategories => Set<RouteCategory>();
	public DbSet<RouteRouteCategory> RouteRouteCategories => Set<RouteRouteCategory>();
	public DbSet<Attachment> Attachments => Set<Attachment>();
	public DbSet<RouteExampleRecord> RouteExampleRecords => Set<RouteExampleRecord>();
	
	public ApplicationDbContext() { }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		SeedAdmin(modelBuilder);
	}

	private void SeedAdmin(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Admin>().HasData(
			new Admin
			{
				// password = qwerty12345
				Id = 1,
				Email = "admin@digitaldiary.site",
				PasswordHash =
					"F3B02422468A5817692D96C01BFC510FEF87BDAD18598B8579B3018E21A1E4C6C8A7ADC7EC84A63405ABB8F7207D9E22263995633514BA700829B2B68B23D8B1",
				PasswordSalt =
					new byte[] { 227, 136, 70, 21, 69, 106, 227, 97, 185, 173, 51, 248, 75, 22, 34, 133 },
				FirstName = "Admin",
				SecondName = "Admin",
				Type = AdminType.Super,
			});
	}
}
