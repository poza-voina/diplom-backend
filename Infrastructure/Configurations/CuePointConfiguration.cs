using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CuePointConfiguration : IEntityTypeConfiguration<CuePoint>
{
	public void Configure(EntityTypeBuilder<CuePoint> builder)
	{
		builder
			.ToTable("cuePoints");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired(true);

		builder
			.Property(x => x.Title)
			.HasColumnName("title")
			.IsRequired(true);

		builder
			.Property(x => x.Description)
			.HasColumnName("description")
			.IsRequired(false);

		builder
			.Property(x => x.CuePointType)
			.HasColumnName("cuePointType")
			.IsRequired(false);

		builder
			.Property(x => x.CreatedAt)
			.HasColumnName("createdAt")
			.HasDefaultValueSql("now() at time zone 'utc'")
			.IsRequired(true);

		builder
			.Property(x => x.RouteId)
			.HasColumnName("routeId")
			.IsRequired(true);

		builder
			.Property(x => x.SortIndex)
			.HasColumnName("sortIndex")
			.IsRequired(true);

		builder
			.Property(x => x.Address)
			.HasColumnName("address")
			.IsRequired(false);

		builder
			.Property(x => x.Latitude)
			.HasColumnName("latitude")
			.IsRequired(false);

		builder
			.Property(x => x.Longitude)
			.HasColumnName("longitude")
			.IsRequired(false);
	}
}