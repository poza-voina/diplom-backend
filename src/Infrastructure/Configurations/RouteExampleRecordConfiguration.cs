using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RouteExampleRecordConfiguration : IEntityTypeConfiguration<RouteExampleRecord>
{
	public void Configure(EntityTypeBuilder<RouteExampleRecord> builder)
	{
		builder
			.ToTable("routeExampleRecords");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.HasColumnName("id");

		builder
			.Property(x => x.ClientId)
			.HasColumnName("clientId");

		builder
			.Property(x => x.RouteExampleId)
			.HasColumnName("routeExampleId");

		builder
			.Property(x => x.CreatedAt)
			.HasColumnName("createdAt")
			.HasDefaultValueSql(DatabaseConfigurationHelper.DefaultDateTime)
			.IsRequired();

		builder
			.Property(x => x.Status)
			.HasColumnName("status")
			.HasConversion<string>();

		builder
			.HasIndex(
				x => new
				{
					x.RouteExampleId,
					x.ClientId
				})
			.IsUnique();

		builder
			.HasOne(x => x.Client)
			.WithMany(c => c.RouteExampleRecords)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasOne(x => x.RouteExample)
			.WithMany(x => x.RouteExampleRecords)
			.HasForeignKey(x => x.RouteExampleId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

