using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Entities;

namespace Core.Entities;

public class User : BaseEntity
{
	public User(long id,
			string email,
			string firstName,
			string secondName,
			string patronymic,
			int age,
			string phoneNumber,
			DateTime registrationDateTime,
			bool isEmailConfirmed,
			string password) : base(id)
	{
		Email = email;
		FirstName = firstName;
		SecondName = secondName;
		Patronymic = patronymic;
		Age = age;
		PhoneNumber = phoneNumber;
		RegistrationDateTime = registrationDateTime;
		IsEmailConfirmed = isEmailConfirmed;
		Password = password;
	}

	[Column("Email")]
	public string Email { get; set; }

	[Column("FirstName")]
	public string FirstName { get; set; }

	[Column("SecondName")]
	public string SecondName { get; set; }

	[Column("Patronymic")]
	public string Patronymic { get; set; }

	[Column("Age")]
	public int Age { get; set; }

	[Column("PhoneNumber")]
	public string PhoneNumber { get; set; }

	[Column("RegistrationDateTime")]
	public DateTime RegistrationDateTime { get; set; }

	[Column("IsEmailConfirmed")]
	public bool IsEmailConfirmed { get; set; }

	[Column("Password")]
	public string Password { get; set; }
}
