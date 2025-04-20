using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
	public void Configure(EntityTypeBuilder<Route> builder)
	{
		builder
			.HasMany(e => e.RouteExamples)
			.WithOne(e => e.Route)
			.HasForeignKey(e => e.RouteId)
			.IsRequired();

		builder
			.HasMany(x => x.RouteCategories)
			.WithMany(x => x.Routes)
			.UsingEntity<RouteRouteCategory>();

		builder
			.ToTable("routes");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(x => x.Title)
			.HasColumnName("title")
			.IsRequired();

		builder
			.Property(x => x.Description)
			.HasColumnName("description");

		builder
			.Property(x => x.MaxCountPeople)
			.HasColumnName("maxCountPeople");

		builder
			.Property(x => x.MinCountPeople)
			.HasColumnName("minCountPeople");

		builder
			.Property(x => x.BaseCost)
			.HasColumnName("baseCost");

		builder
			.Property(x => x.CreationDateTime)
			.HasColumnName("creationDateTime")
			.HasDefaultValueSql("now() at time zone 'utc'")
			.IsRequired();

		builder
			.Property(x => x.IsHidden)
			.HasColumnName("isHidden")
			.IsRequired();
	}
}

