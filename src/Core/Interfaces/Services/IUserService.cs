using System.Security.Claims;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;

public interface IUserService
{
	Task<Client> GetClientAsync(ClaimsPrincipal claim);
}
