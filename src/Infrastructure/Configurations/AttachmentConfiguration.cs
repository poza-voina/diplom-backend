namespace Infrastructure.Configurations;

using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
	public void Configure(EntityTypeBuilder<Attachment> builder)
	{
		builder.ToTable("attachments");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired();

		builder.Property(x => x.FileName)
			.HasColumnName("fileName")
			.IsRequired();

		builder.Property(x => x.Uri)
			.HasColumnName("uri")
			.IsRequired();

		builder.Property(x => x.CreatedAt)
			.HasColumnName("createdAt")
			.HasDefaultValueSql("now() at time zone 'utc'")
			.IsRequired();

		builder.Property(x => x.RouteId)
			.HasColumnName("routeId");

		builder.Property(x => x.CuePointId)
			.HasColumnName("cuePointId");

		builder
			.HasOne(x => x.Route)
			.WithOne(r => r.Attachment)
			.HasForeignKey<Attachment>(x => x.RouteId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasOne(x => x.CuePoint)
			.WithOne(c => c.Attachment)
			.HasForeignKey<Attachment>(x => x.CuePointId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}