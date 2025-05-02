using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
	public void Configure(EntityTypeBuilder<Admin> builder)
	{
		builder
			.ToTable("admin");

		builder
			.HasKey(x => x.Id);

		builder
			.HasIndex(x => x.Email)
			.IsUnique();

		builder
			.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired(true);

		builder
			.Property(x => x.Email)
			.HasColumnName("email")
			.IsRequired(true);

		builder
			.Property(x => x.FirstName)
			.HasColumnName("firstName")
			.IsRequired(true);

		builder
			.Property(x => x.SecondName)
			.HasColumnName("secondName")
			.IsRequired(true);

		builder
			.Property(x => x.PasswordHash)
			.HasColumnName("passwordHash")
			.IsRequired();

		builder
			.Property(x => x.PasswordSalt)
			.HasColumnName("passwordSalt")
			.IsRequired();

		builder
			.Property(x => x.Type)
			.HasColumnName("type")
			.HasConversion<string>();
	}
}