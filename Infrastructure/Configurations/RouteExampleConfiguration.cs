using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RouteExampleConfiguration : IEntityTypeConfiguration<RouteExample>
{
	public void Configure(EntityTypeBuilder<RouteExample> builder)
	{
		builder
			.ToTable("routeExamples");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(x => x.RouteId)
			.HasColumnName("routeId")
			.IsRequired();

		builder
			.Property(x => x.CreationDateTime)
			.HasColumnName("creationDateTime")
			.HasDefaultValueSql("now() at time zone 'utc'")
			.IsRequired();

		builder
			.Property(x => x.StartDateTime)
			.HasColumnName("startDateTime")
			.IsRequired();

		builder
			.Property(x => x.EndDateTime)
			.HasColumnName("endDateTime")
			.IsRequired();
	}
}