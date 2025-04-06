using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Manager
{
	public long Id { get; set; }

	public required string Email { get; set; }

	public required string PhoneNumber { get; set; }

	public required string FirstName { get; set; }

	public required string SecondName { get; set; }

	public required string Patronymic { get; set; }

	public required string Password { get; set; }
}
