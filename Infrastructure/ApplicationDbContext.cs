using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
	DbSet<Route> Routes => Set<Route>();
	DbSet<CuePoint> CuePoints => Set<CuePoint>();
	DbSet<RouteExample> RouteExample => Set<RouteExample>();

	public ApplicationDbContext() { }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Entity<Route>()
			.HasMany(e => e.RouteExamples)
			.WithOne(e => e.Route)
			.HasForeignKey(e => e.RouteId)
			.IsRequired();
	}
}
