using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/routes")]
public class RouteController(IRouteService routeService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetRoutes()
	{
		return Ok(await routeService.GetPerPage(1, 10));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetRouteById(long id)
	{
		return Ok(await routeService.GetAsync(id));
	}

	[HttpPost]
	public async Task<IActionResult> CreateRoute(RouteDto dto)
	{
		return Ok(await routeService.CreateAsync(dto));
	}

	[HttpPut]
	public async Task<IActionResult> UpdateRoute(RouteDto dto)
	{
		return Ok(await routeService.UpdateAsync(dto));
	}
}
