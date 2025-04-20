using Core.Dto.Route;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Api.Controllers;

[ApiController]
[Route("api/client/routes")]

public class RouteController(
	IRouteService routeService,
	IRouteExampleService routeExampleService,
	ICuePointService cuePointService) : ControllerBase
{
	[HttpGet]
	public async Task<IResult> GetRoutes([FromQuery] GetRoutesRequest dto)
	{
		return Results.Ok(await routeService.GetRoutesAsync(dto));
	}

	[HttpGet("{id}")]
	public async Task<IResult> GetRouteById(long id)
	{
		return Results.Ok(await routeService.GetAsync(id));
	}

	[HttpGet("{id}/examples")]
	public async Task<IResult> GetRouteExamples(long id)
	{
		return Results.Ok(await routeExampleService.GetExamplesByRouteId(id));
	}

	[HttpGet("{id}/cue-points")]
	public async Task<IResult> GetRouteCuePoints(long id)
	{
		return Results.Ok(cuePointService.GetAllCuePointsFromRoute(id));
	}


	[HttpPost]
	[Authorize]
	public Task<IResult> ToggleFavoriteRoute()
	{
		throw new NotImplementedException();
	}

	[HttpPost]
	[Authorize]
	public Task<IResult> BookAsync()
	{
		throw new NotImplementedException();
	}
}
