using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

public class UserService(IRepository<User> userRepository, IConfiguration configuration, IPasswordManager passwordManager) : IUserService
{

	public async Task<string> GetJwtToken(GetJwtTokenRequest dto)
	{
		var user = await userRepository.Items.FirstOrDefaultAsync(x => x.Email == dto.Email) ?? throw new InvalidOperationException("User Not Found");

		if (!passwordManager.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
		{
			throw new InvalidOperationException("Password not right");
		}

		return GenerateJwtToken(user.Email);
	}

	public async Task<UserProfileDto> GetProfileAsync(long id)
	{
		var entity = await userRepository.GetAsync(id);
		return entity.Adapt<UserProfileDto>();
	}

	public async Task RegistrationAsync(RegistrationRequest dto)
	{
		var user = dto.Adapt<User>();
		user.PasswordHash = passwordManager.GetPasswordHash(dto.Password, out var salt);
		user.PasswordSalt = salt;

		await userRepository.CreateAsync(user);
	}

	private string GenerateJwtToken(string email)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var token = new JwtSecurityToken(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

}
