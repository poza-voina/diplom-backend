using Core.Dto;

namespace Core.Interfaces.Services;

public interface IClientService
{
	Task RegistrationAsync(RegistrationRequest dto);
	Task<UserProfileDto> GetProfileAsync(long id);
}