using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dto.Auth;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class AuthService(
	IRepository<Client> clientRepository,
	IRepository<Admin> adminRepository,
	IConfiguration configuration,
	IPasswordManager passwordManager) : IAuthService
{

	public async Task<string> AuthenticateClientAsync(LoginRequest request)
	{
		var user = await clientRepository.Items.FirstOrDefaultAsync(x => x.Email == request.Email) ?? throw new EntityNotFoundException("Пользователь не найден");

		if (!passwordManager.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
		{
			throw new InvalidOperationException("Password not right");
		}

		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, "Client")
			};

		return GenerateJwtToken(claims);
	}

	public async Task<string> AuthenticateAdminAsync(LoginRequest request)
	{
		var user = await adminRepository.Items.FirstOrDefaultAsync(x => x.Email == request.Email) ?? throw new EntityNotFoundException("Пользователь не найден");

		if (!passwordManager.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
		{
			throw new InvalidPasswordException("Password not right");
		}

		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, "Admin")
			};

		return GenerateJwtToken(claims);
	}

	private string GenerateJwtToken(IEnumerable<Claim> claims)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");
		
		var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Settings Not Found");
		var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Settings Not Found");
		var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Settings Not Found");
		var expireMinutes = jwtSettings["ExpireMinutes"] ?? throw new InvalidOperationException("JWT Settings Not Found");
		
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: issuer,
			audience: audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(expireMinutes)),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
