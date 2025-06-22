using Core.Dto.Client;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Humanizer;
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

    public async Task UpdateProfileAsync(long id, RegistrationRequest request)
	{
		var user = request.Adapt<Client>();
		user.Id = id;
        user.PasswordHash = passwordManager.GetPasswordHash(request.Password, out var salt);
        user.PasswordSalt = salt;

        await userRepository.UpdateAsync(user);
	}

}