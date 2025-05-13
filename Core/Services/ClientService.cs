using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class ClientService(IRepository<Client> userRepository, IConfiguration configuration, IPasswordManager passwordManager) : IClientService
{
	public async Task<UserProfileDto> GetProfileAsync(long id)
	{
		var entity = await userRepository.GetAsync(id);
		return entity.Adapt<UserProfileDto>();
	}

	public async Task RegistrationAsync(RegistrationRequest dto)
	{
		var user = dto.Adapt<Client>();
		user.PasswordHash = passwordManager.GetPasswordHash(dto.Password, out var salt);
		user.PasswordSalt = salt;

		await userRepository.CreateAsync(user);
	}
}
