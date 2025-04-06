namespace Core.Dto;

public class UserProfileDto
{
	public required string Email { get; set; }
	public required string FirstName { get; set; }
	public required string SecondName { get; set; }
	public string? Patronymic { get; set; }
	public required string PhoneNumber { get; set; }
	public DateTime RegistrationDateTime { get; set; }
	public required bool IsEmailConfirmed { get; set; }
}
