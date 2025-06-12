using Core.Dto.Auth;

namespace Core.Interfaces.Services;

public interface IAuthService
{
	Task<string> AuthenticateAdminAsync(LoginRequest request);
	Task<string> AuthenticateClientAsync(LoginRequest request);
}
