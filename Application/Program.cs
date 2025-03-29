using System.Text;

using Infrastructure.Extensions;


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

services.AddDbContext("User ID=postgres;Password=psql;Server=localhost;Port=1111;Database=Ural;Include Error Detail=true");

services.AddRepositories();
services.AddServices();

var app = builder.Build();

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