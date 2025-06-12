using System.Security.Claims;
using Core.Dto;
using Core.Dto.Route;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Api.Controllers;

[ApiController]
[Route("api/client/actions")]
[Authorize(Roles = "Client")]
public class ClientActionsController(
	IRouteService routeService,
	IRouteExampleService routeExampleService,
	ICuePointService cuePointService,
	IUserService userService,
	IBookService bookService) : ControllerBase
{
	[HttpPost("books/{routeExampleId:long}")]
	public async Task<IResult> BookAsync([FromRoute] long routeExampleId)
	{
		var client = await userService.GetClientAsync(User);
		await routeExampleService.BookAsync(client, routeExampleId);

		return Results.Ok();
		
	}

	[HttpGet("books")]
	public async Task<IResult> GetBooksAsync([FromQuery] GetBooksRequest request)
	{
		var client = await userService.GetClientAsync(User);
		var result = await bookService.GetBooksAsync(client, request);

		return Results.Ok(result);
	}

	[HttpGet("books/{bookId:long}")]
	public async Task<IResult> GetBookAsync([FromRoute] long bookId)
	{
		var client = await userService.GetClientAsync(User);
		var result = await bookService.GetBookAsync(client, bookId);

		return Results.Ok(result);
	}

	[HttpDelete("books/{routeExampleId:long}")]
	public async Task<IResult> UnBookAsync([FromRoute] long routeExampleId)
	{
		var client = await userService.GetClientAsync(User);
		await routeExampleService.UnBookAsync(client, routeExampleId);
		return Results.Ok();
	}
}
