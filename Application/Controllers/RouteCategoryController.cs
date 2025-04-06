using Core.Dto.RouteCategory;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/categories")]
public class RouteCategoryController(IRouteCategoryService routeCategoryService) : ControllerBase
{
	[HttpGet("{id:long}")]
	public async Task<IResult> GetAsync([FromRoute] long id)
	{
		var routeCategory = await routeCategoryService.GetAsync(id);
		return Results.Ok(routeCategory);
	}

	[HttpPost]
	public async Task<IResult> CreateAsync([FromBody] CreateRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.CreateAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpPut]
	public async Task<IResult> UpdateAsync([FromBody] UpdateRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.UpdateAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpGet]
	public async Task<IResult> FilterAsync([FromQuery] FilterRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.FilterAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpDelete]
	public async Task<IResult> DeleteAsync([FromRoute] long id)
	{
		await routeCategoryService.DeleteAsync(id);
		return Results.Ok();
	}
}
