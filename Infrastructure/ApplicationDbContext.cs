using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
	DbSet<Route> Routes => Set<Route>();
	DbSet<CuePoint> CuePoints => Set<CuePoint>();

	public ApplicationDbContext() { }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
}
