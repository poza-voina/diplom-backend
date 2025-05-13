using Core.Dto;
using Core.Dto.Auth;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Api.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(IClientService userService, IAuthService authService) : ControllerBase
{
	[HttpPost("auth/register")]
	public async Task<IResult> RegistrationAsync([FromBody] RegistrationRequest request)
	{
		await userService.RegistrationAsync(request);
		return Results.Ok();
	}

	[HttpPost("auth/login")]
	public async Task<IResult> GetJwtAsync([FromBody] LoginRequest request)
	{
		var token = await authService.AuthenticateClientAsync(request);
		return Results.Ok(token);
	}

	[Authorize(Roles = "Client")]
	[HttpGet("profile/{id:long}")]
	public async Task<IResult> GetProfileAsync([FromRoute] long id)
	{
		var profile = await userService.GetProfileAsync(id);
		return Results.Ok(profile);
	}
}