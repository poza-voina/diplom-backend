using Core.Dto;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Api.Controllers;

[ApiController]
[Route("api/client/users")]
public class UserController(IUserService userService) : ControllerBase
{
	[HttpPost("register")]
	public async Task<IResult> RegistrationAsync([FromBody] RegistrationRequest request)
	{
		await userService.RegistrationAsync(request);
		return Results.Ok();
	}

	[HttpPost("authorize")]
	public async Task<IResult> GetJwtAsync([FromBody] GetJwtTokenRequest request)
	{
		var token = await userService.GetJwtToken(request);
		return Results.Ok(token);
	}

	[Authorize]
	[HttpGet("profile/{id:long}")]
	public async Task<IResult> GetProfileAsync([FromRoute] long id)
	{
		var profile = await userService.GetProfileAsync(id);
		return Results.Ok(profile);
	}
}