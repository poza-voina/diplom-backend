using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;
using Core.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Api.Controllers;

[Route("api/admin/map")]
[Authorize(Roles = "Admin")]
public class MapController(IMapService mapService) : ControllerBase
{
	[HttpGet("address")]
	public async Task<IActionResult> GetAddressWithCoords([FromQuery] AddressDto dto)
	{
		return Ok(await mapService.GetAddressWithCoordsAsync(dto));
	}
}
