using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Middlewares;

public class DatabaseExceptionTranslationMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<DatabaseExceptionTranslationMiddleware> _logger;

	public DatabaseExceptionTranslationMiddleware(RequestDelegate next, ILogger<DatabaseExceptionTranslationMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (DbUpdateException dbEx) when (dbEx.InnerException is PostgresException pgEx)
		{
			if (pgEx.SqlState == "23505")
			{
				throw new EntityAlReadyExists("Уже существует");
			}

			throw;
		}
	}
}
