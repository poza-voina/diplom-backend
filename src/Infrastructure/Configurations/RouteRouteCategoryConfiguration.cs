using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RouteRouteCategoryConfiguration : IEntityTypeConfiguration<RouteRouteCategory>
{
	public void Configure(EntityTypeBuilder<RouteRouteCategory> builder)
	{
		builder
			.ToTable("routeRouteCategory");
		
		builder
			.HasKey(x => new { x.RouteId, x.RouteCategoryId });
	}
}
