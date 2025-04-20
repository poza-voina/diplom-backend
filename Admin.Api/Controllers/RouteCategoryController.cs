using Core.Dto.RouteCategory;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class RouteCategoryController(IRouteCategoryService routeCategoryService) : ControllerBase
{
	[HttpGet("{id:long}")]
	[Authorize]
	public async Task<IResult> GetAsync([FromRoute] long id)
	{
		var routeCategory = await routeCategoryService.GetAsync(id);
		return Results.Ok(routeCategory);
	}

	[HttpPost]
	[Authorize]
	public async Task<IResult> CreateAsync([FromBody] CreateRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.CreateAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpPut]
	[Authorize]
	public async Task<IResult> UpdateAsync([FromBody] UpdateRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.UpdateAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpGet]
	[Authorize]
	public async Task<IResult> GetAll()
	{
		var categories = await routeCategoryService.GetAllAsync();
		return Results.Ok(categories);
	}

	[HttpGet("filter")]
	[Authorize]
	public async Task<IResult> FilterAsync([FromQuery] FilterRouteCategoryRequest dto)
	{
		var routeCategory = await routeCategoryService.FilterAsync(dto);
		return Results.Ok(routeCategory);
	}

	[HttpDelete]
	[Authorize]
	public async Task<IResult> DeleteAsync([FromRoute] long id)
	{
		await routeCategoryService.DeleteAsync(id);
		return Results.Ok();
	}
}
