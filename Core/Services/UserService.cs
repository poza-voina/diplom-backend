using System.Security.Claims;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserService(IRepository<Client> clientRepository) : IUserService
{
	public async Task<Client> GetClientAsync(ClaimsPrincipal claim)
	{
		var email = claim.FindFirst(ClaimTypes.Name)?.Value;

		var client = await clientRepository.Items.SingleOrDefaultAsync(x => x.Email == email) ?? throw new EntityNotFoundException("Не удалось найти пользователя.");

		return client;
	}
}
