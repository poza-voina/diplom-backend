using Core.Dto.Auth;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/admin/auth")]
public class AdminAuthController(IAuthService authService) : ControllerBase
{
	[HttpPost("login")]
	public async Task<IResult> Login([FromBody] LoginRequest request)
	{
		return Results.Ok(await authService.AuthenticateAdminAsync(request));
	}
}