using Core.Dto.Auth;
using Core.Dto.Client;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Api.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(IClientService clientService, IAuthService authService, IUserService userService) : ControllerBase
{
	[HttpPost("auth/register")]
	public async Task<IResult> RegistrationAsync([FromBody] RegistrationRequest request)
	{
		await clientService.RegistrationAsync(request);
		return Results.Ok();
	}

	[HttpPost("auth/login")]
	public async Task<IResult> GetJwtAsync([FromBody] LoginRequest request)
	{
		var token = await authService.AuthenticateClientAsync(request);
		return Results.Ok(token);
	}

	[Authorize(Roles = "Client")]
	[HttpGet("profile")]
	public async Task<IResult> GetProfileAsync()
	{
		var client = await userService.GetClientAsync(User);
		var profile = await clientService.GetProfileAsync(client.Id!.Value);
		return Results.Ok(profile);
	}
}