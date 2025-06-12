using System.Text;
using Application.Exceptions;
using Application.Middlewares;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Core.Services;
using Minio;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Services.AddControllers();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
	options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
	options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});


services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Введите токен JWT в формате: Bearer {token}"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

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

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

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
			IssuerSigningKey = new SymmetricSecurityKey(
	Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey") ?? throw new Exception("Secret not found"))),
			ValidateIssuerSigningKey = true,

			RoleClaimType = ClaimTypes.Role,
			NameClaimType = ClaimTypes.Email
		};
	});

services.AddAuthorization();
services.AddExceptionHandler<ExceptionHandler>();
services.AddSingleton<IMinioClient>(sp =>
{
	var config = sp.GetRequiredService<IConfiguration>();
	return new MinioClient()
		.WithEndpoint(config["Minio:Endpoint"])
		.WithCredentials(config["Minio:AccessKey"], config["Minio:SecretKey"])
		.WithSSL(false)
		.Build();
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext(connectionString);

services.AddRepositories();
services.AddServices();
services.AddProblemDetails();


var app = builder.Build();

app.UseMiddleware<DatabaseExceptionTranslationMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();