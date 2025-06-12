using Core.Dto.Client;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure;
using Infrastructure.Entities;
using Mapster;

namespace Core.Services;

public class ClientService(
	IRepository<Client> userRepository,
	IPasswordManager passwordManager) : IClientService
{
	public async Task<ClientProfileDto> GetProfileAsync(long id)
	{
		var entity = await userRepository.GetAsync(id);
		return entity.Adapt<ClientProfileDto>();
	}

	public async Task RegistrationAsync(RegistrationRequest dto)
	{
		var user = dto.Adapt<Client>();
		user.PasswordHash = passwordManager.GetPasswordHash(dto.Password, out var salt);
		user.PasswordSalt = salt;

		await userRepository.CreateAsync(user);
	}
}