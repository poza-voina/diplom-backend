using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
	public void Configure(EntityTypeBuilder<Manager> builder)
	{
		builder
			.ToTable("managers");

		builder
			.HasKey(x => x.Id);

		builder
			.HasIndex(x => x.Email)
			.IsUnique();

		builder
			.HasIndex(x => x.PhoneNumber)
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
			.Property(x => x.PhoneNumber)
			.HasColumnName("phoneNumber")
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
			.Property(x => x.Patronymic)
			.HasColumnName("patronymic")
			.IsRequired(false);

		builder
			.Property(x => x.Password)
			.HasColumnName("password")
			.IsRequired(true);
	}
}