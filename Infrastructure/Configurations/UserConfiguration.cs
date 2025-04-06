using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
	{
		builder
			.HasIndex(x => x.Email)
			.IsUnique();

		builder
			.HasIndex(x => x.PhoneNumber)
			.IsUnique();

		builder
			.Property(x => x.RegistrationDateTime)
			.HasDefaultValueSql("now()");

		builder
			.ToTable("users");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(x => x.Email)
			.HasColumnName("email")
			.IsRequired();

		builder
			.Property(x => x.FirstName)
			.HasColumnName("firstName")
			.IsRequired();

		builder
			.Property(x => x.SecondName)
			.HasColumnName("secondName")
			.IsRequired();

		builder
			.Property(x => x.Patronymic)
			.HasColumnName("patronymic");

		builder
			.Property(x => x.PhoneNumber)
			.HasColumnName("phoneNumber")
			.IsRequired();

		builder
			.Property(x => x.RegistrationDateTime)
			.HasColumnName("registrationDateTime")
			.HasDefaultValueSql("now() at time zone 'utc'")
			.IsRequired();

		builder
			.Property(x => x.IsEmailConfirmed)
			.HasColumnName("isEmailConfirmed")
			.IsRequired();

		builder
			.Property(x => x.PasswordHash)
			.HasColumnName("passwordHash")
			.IsRequired();

		builder
			.Property(x => x.PasswordSalt)
			.HasColumnName("passwordSalt")
			.IsRequired();
	}
}