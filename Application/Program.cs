using System.Text;
using Application.Exceptions;
using Application.Middlewares;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularApp",
		policy =>
		{
			policy.WithOrigins("http://localhost:4200")
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

services.AddHttpClient("Geocoder")
	.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
	{
		SslProtocols = System.Security.Authentication.SslProtocols.Tls12
	});

var jwtSettings = builder.Configuration.GetSection("Authorization");

services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtSettings.GetValue<string>("Issuer") ?? throw new Exception("Issuer not found"), // Исправлено с "Issure" на "Issuer"
			ValidateAudience = true,
			ValidAudience = jwtSettings.GetValue<string>("Audience") ?? throw new Exception("Audience not found"),
			ValidateLifetime = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Audience") ?? throw new Exception("Audience not found"))),
			ValidateIssuerSigningKey = true
		};
	});

services.AddAuthorization();
services.AddExceptionHandler<ExceptionHandler>();


services.AddDbContext("User ID=postgres;Password=psql;Server=localhost;Port=1111;Database=Ural;Include Error Detail=true");

services.AddRepositories();
services.AddServices();
services.AddProblemDetails();


var app = builder.Build();

app.UseMiddleware<DatabaseExceptionTranslationMiddleware>();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();