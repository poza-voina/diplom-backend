using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("managers")]
public class ManagerController : ControllerBase
{
	[HttpGet("{id:long}")]
	public async Task<IResult> GetAsync()
	{
		return Results.Ok();
	}

	[HttpPost]
	public async Task<IResult> CreateAsync()
	{
		return Results.Ok();
	}

	[HttpPut]
	public async Task<IResult> UpdateAsync()
	{
		return Results.Ok();
	}

	[HttpDelete]
	public async Task<IResult> DeleteAsync()
	{
		return Results.Ok();
	}
}
