using Core.Dto;
using Core.Dto.CuePoint;
using Core.Dto.Route;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/routes")]
public class RouteController(IRouteService routeService, IRouteExampleService routeExampleService, ICuePointService cuePointService, IRouteCategoryService routeCategoryService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetRoutes([FromQuery] GetRoutesRequest dto)
	{
		return Ok(await routeService.GetRoutesAsync(dto));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetRouteById(long id)
	{
		return Ok(await routeService.GetAsync(id));
	}

	[HttpGet("{id}/examples")]
	public async Task<IActionResult> GetRouteExamples(long id)
	{
		var result = await routeExampleService.GetExamplesByRouteId(id);
		return Ok(result);
	}

	[HttpGet("{id}/cue-points")]
	public IActionResult GetRouteCuePoints(long id)
	{
		return Ok(cuePointService.GetAllCuePointsFromRoute(id));
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> CreateRoute([FromBody] CreateRouteRequest dto)
	{
		return Ok(await routeService.CreateAsync(dto));
	}

	[HttpDelete("{id:long}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteRoute([FromRoute] long id)
	{
		await routeService.DeleteAsync(id);
		return Ok();
	}


	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> UpdateRoute([FromBody] UpdateRouteRequest dto)
	{
		return Ok(await routeService.UpdateAsync(dto));
	}

	[HttpPut("{routeId:long}/update-cue-points")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> UpdateRouteCuePoints([FromBody] IEnumerable<CuePointDto> dto, [FromRoute] long routeId)
	{
		var result = await cuePointService.UpdateOrCreateRangeAsync(dto, routeId);
		return Ok(result);
	}

	[HttpGet("visible-routes")]
	public async Task<IResult> GetVisibleRoutes([FromQuery] GetVisibleRoutesRequest request)
	{
		var result = await routeService.GetVisibleRoutesAsync(request);

		return Results.Ok(result);
	}

	[HttpGet("route-examples/by-month")]
	public async Task<IResult> GetRouteExamplesFromMonth([FromQuery] GetRoutesExampleFromMonthRequest request)
	{
		// Валидация
		if (request.Month < 1 || request.Month > 12)
			return Results.BadRequest("Недопустимый месяц.");

		// Пример получения данных
		var examples = await routeExampleService.GetByMonthAsync(request);

		return Results.Ok(examples);
	}
}

