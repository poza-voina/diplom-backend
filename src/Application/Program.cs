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

services.AddSwagger();

var frontendUrl = builder.Configuration.GetValue<string>("Frontend:Url") ?? throw new InvalidOperationException("Не удалось найти URL Frontend");
Console.WriteLine(frontendUrl);
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		policy =>
		{
            policy.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

services.AddHttpClient("Geocoder")
	.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
	{
		SslProtocols = System.Security.Authentication.SslProtocols.Tls12
	});

services.AddCustomAuthentication(builder);
services.AddAuthorization();
services.AddProblemDetails();
services.AddExceptionHandler<ExceptionHandler>();
services.AddMinio();
services.AddDbContext(builder);
services.AddRepositories();
services.AddServices();

var app = builder.Build();
app.UseExceptionHandler();
app.UseMiddleware<DatabaseExceptionTranslationMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();