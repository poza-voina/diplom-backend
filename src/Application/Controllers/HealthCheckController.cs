using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.Exceptions;

[ApiController]
[Route("health")]
public class HealthCheckController(ApplicationDbContext context, IMinioService minioService) : ControllerBase
{
	[HttpGet("infarstucture")]
	public async Task<IActionResult> CheckInfarstucture()
	{
		try
		{
			bool canConnectDb = await context.Database.CanConnectAsync();
			if (!canConnectDb)
				return StatusCode(500, "Cannot connect to database");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Database error: {ex.Message}");
		}

		var minioErrors = await minioService.GetErrorsAsync();

		if (minioErrors is { })
		{
			return StatusCode(500, minioErrors);
		}

		return Ok();
	}

	[HttpGet]
	public IResult Check()
	{
		return Results.Ok();
	}
}
