using Core.Dto.RouteCategory.RouteExample;
using Core.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/route-examples")]
[Authorize(Roles="Admin")]
public class RouteExamplesController(IRouteExampleService routeExampleService) : ControllerBase
{
	[HttpPut]
	public async Task<IResult> CreateOrUpdate([FromBody] RouteExampleCreateOrUpdateRequest request)
	{
		var result = await routeExampleService.CreateOrUpdateAsync(request);
		return Results.Ok(result);
	}

	[HttpPut("by-route")]
	public async Task<IResult> CreateOrUpdateByRoute([FromBody] IEnumerable<RouteExampleCreateOrUpdateRequest> request)
	{
		var result = await routeExampleService.CreateOrUpdateByRouteAsync(request);
		return Results.Ok(result);
	}

	[HttpDelete("{id:long}")]
	public async Task<IResult> DeleteAsync([FromRoute] long id)
	{
		await routeExampleService.DeleteAsync(id);
		return Results.Ok();
	}
}
