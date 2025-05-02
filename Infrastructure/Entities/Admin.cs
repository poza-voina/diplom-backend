using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class Admin : BaseEntity
{
	public required string FirstName { get; set; }

	public required string SecondName { get; set; }

	public required string Email { get; set; }

	public required string PasswordHash { get; set; }

	public required byte[] PasswordSalt { get; set; }

	public required AdminType Type { get; set; }
}