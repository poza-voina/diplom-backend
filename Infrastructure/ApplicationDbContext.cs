using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
	public DbSet<Route> Routes => Set<Route>();
	public DbSet<CuePoint> CuePoints => Set<CuePoint>();
	public DbSet<RouteExample> RouteExample => Set<RouteExample>();
	public DbSet<User> Users => Set<User>();
	public DbSet<RouteCategory> RouteCategories => Set<RouteCategory>();
	public DbSet<RouteRouteCategory> RouteRouteCategories => Set<RouteRouteCategory>();

	public ApplicationDbContext() { }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

	}
}
