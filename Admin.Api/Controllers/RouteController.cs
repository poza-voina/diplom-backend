using Core.Dto;
using Core.Dto.Route;
using Core.Dto.RouteCategory;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers;

[Route("api/admin/routes")]
[Authorize(Roles = "Admin")]
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
		return Ok(await routeExampleService.GetExamplesByRouteId(id));
	}

	[HttpGet("{id}/cue-points")]
	public IActionResult GetRouteCuePoints(long id)
	{
		return Ok(cuePointService.GetAllCuePointsFromRoute(id));
	}

	[HttpPost]
	public async Task<IActionResult> CreateRoute([FromBody] CreateRouteRequest dto)
	{
		return Ok(await routeService.CreateAsync(dto));
	}

	[HttpPut]
	public async Task<IActionResult> UpdateRoute([FromBody] UpdateRouteRequest dto)
	{
		return Ok(await routeService.UpdateAsync(dto));
	}

	[HttpPut("update-cue-points")]
	public async Task<IActionResult> UpdateRouteCuePoints([FromBody] IEnumerable<CuePointDto> dto)
	{
		await cuePointService.UpdateOrCreateRangeAsync(dto);
		return Ok();
	}
}

