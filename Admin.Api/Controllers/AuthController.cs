using Core.Dto.Auth;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admin.Api.Controllers;

public class AuthController(IAuthService authService)
{
	[HttpPost("login")]
	public async Task<IResult> Login([FromBody] LoginRequest request)
	{
		return Results.Ok(await authService.AuthenticateAdminAsync(request));
	}
}