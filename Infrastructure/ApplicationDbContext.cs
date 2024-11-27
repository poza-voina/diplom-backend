using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
	
	ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
