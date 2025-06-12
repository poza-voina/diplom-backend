using Core.Exceptions;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Exceptions;

public class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
	/// <inheritdoc/>
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		var problemDetails = new ProblemDetails()
		{
			Title = exception.Message,
			Detail = exception.StackTrace,
		};

		if (exception is EntityNotFoundException)
		{
			problemDetails.Type = nameof(Results.NotFound);
			problemDetails.Status = StatusCodes.Status404NotFound;
		}
		else if (exception is ApiBaseException or InfrastructureBaseException)
		{
			problemDetails.Type = nameof(Results.BadRequest);
			problemDetails.Status = StatusCodes.Status400BadRequest;
		}
		else
		{
			problemDetails.Type = "InternalServerError";
			problemDetails.Status = StatusCodes.Status500InternalServerError;
		}

		if (problemDetails is { Status: not null })
		{
			httpContext.Response.StatusCode = problemDetails.Status.Value;
			return await problemDetailsService.TryWriteAsync(new()
			{
				HttpContext = httpContext,
				ProblemDetails = problemDetails,
			});
		}
		else
		{
			return true;
		}

	}
}