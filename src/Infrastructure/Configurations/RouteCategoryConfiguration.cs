using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RouteCategoryConfiguration : IEntityTypeConfiguration<RouteCategory>
{
	public void Configure(EntityTypeBuilder<RouteCategory> builder)
	{
		builder
			.ToTable("routeCategories");

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
			.HasIndex(x => x.Title)
			.IsUnique();

		builder
			.Property(x => x.Description)
			.HasColumnName("description");
	}
}