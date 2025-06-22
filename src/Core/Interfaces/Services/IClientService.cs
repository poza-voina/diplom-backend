using Core.Dto.Client;

namespace Core.Interfaces.Services;

public interface IClientService
{
	Task RegistrationAsync(RegistrationRequest dto);
	Task<ClientProfileDto> GetProfileAsync(long id);
    Task UpdateProfileAsync(long id, RegistrationRequest request);
}