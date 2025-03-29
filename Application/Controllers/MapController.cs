using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;
using Core.Dto;

namespace Application.Controllers;

[Route("api/map")]
public class MapController(IMapService mapService) : ControllerBase
{
	[HttpGet("key")]
	public Task<IActionResult> GetYandexApiKey()
	{
		throw new NotImplementedException();
	}

	[HttpGet("address")]
	public async Task<IActionResult> GetAddressWithCoords([FromQuery] AddressDto dto)
	{
		return Ok(await mapService.GetAddressWithCoordsAsync(dto));
	}
}
