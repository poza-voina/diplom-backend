using Core.Dto.Admin;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers;

[ApiController]
[Route("api/admin/admins")]
[Authorize(Roles = "Admin")]
public class AdminController(IAdminService adminService) : ControllerBase
{
	[HttpGet("{id:long}")]
	public async Task<IResult> GetAsync([FromQuery] long id)
	{
		var admin = await adminService.GetAsync(id);
		
		return Results.Ok(admin);
	}

	[HttpGet]
	public async Task<IResult> GetAsync([FromQuery] GetAllAdminRequest request)
	{
		var admin = await adminService.GetAllAsync(request);

		return Results.Ok(admin);
	}


	[HttpPost]
	public async Task<IResult> CreateAsync([FromBody] CreateAdminRequest request)
	{
		var admin = await adminService.CreateAsync(request);

		return Results.Ok(admin);
	}

	[HttpPut]
	public async Task<IResult> UpdateAsync([FromBody] UpdateAdminRequest request)
	{
		var admin = await adminService.UpdateAsync(request);

		return Results.Ok(admin);
	}

	[HttpDelete]
	public async Task<IResult> DeleteAsync([FromQuery] long id)
	{
		await adminService.DeleteAsync(id);
		
		return Results.Ok();
	}
}
