using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class User : BaseEntity
{
	public required string Email { get; set; }
	public required string FirstName { get; set; }
	public required string SecondName { get; set; }
	public string? Patronymic { get; set; }
	public required string PhoneNumber { get; set; }
	public DateTime RegistrationDateTime { get; set; }
	public required bool IsEmailConfirmed { get; set; }
	public required string PasswordHash { get; set; }
	public required byte[] PasswordSalt { get; set; }
}
