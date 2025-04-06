using Core.Dto;

namespace Core.Interfaces.Services;

public interface IUserService
{
	Task RegistrationAsync(RegistrationRequest dto);
	Task<string> GetJwtToken(GetJwtTokenRequest dto);
	Task<UserProfileDto> GetProfileAsync(long id);
}
