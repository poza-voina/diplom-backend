using Core.Dto;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
[Route("api/route-example-records")]
[Authorize(Roles="Admin")]
public class RouteExamplesRecordController(IRouteExampleRecordService routeExampleRecordService) : ControllerBase
{
	[HttpGet("filter")]
	public async Task<IResult> FilterAsync([FromQuery]RouteExamplesRecordFilterRequest request)
	{
		var result = await routeExampleRecordService.FilterAsync(request);

		return Results.Ok(result);
	}
}