using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Entities;

namespace Core.Entities;

public class User : BaseEntity
{
	[Column("Email")]
	public required string Email { get; set; }

	[Column("FirstName")]
	public required string FirstName { get; set; }

	[Column("SecondName")]
	public required string SecondName { get; set; }

	[Column("Patronymic")]
	public required string Patronymic { get; set; }

	[Column("Age")]
	public required int Age { get; set; }

	[Column("PhoneNumber")]
	public required string PhoneNumber { get; set; }

	[Column("RegistrationDateTime")]
	public required DateTime RegistrationDateTime { get; set; }

	[Column("IsEmailConfirmed")]
	public required bool IsEmailConfirmed { get; set; }

	[Column("Password")]
	public required string Password { get; set; }
}
