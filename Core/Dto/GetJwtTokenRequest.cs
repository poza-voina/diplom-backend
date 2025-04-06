namespace Core.Dto;

public class GetJwtTokenRequest
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}
