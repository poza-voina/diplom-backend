using Core.Dto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.VisualBasic;

namespace Core.Services;

public class CuePointService(ICuePointRepository repository) : ICuePointService
{
	private ICuePointRepository _repository = repository;

	public async Task<CuePoint> CreateCuePoint(CuePoint cuePoint)
	{
		return await _repository.CreateAsync(cuePoint);
	}

	public async Task DeleteCuePoint(CuePoint cuePoint)
	{
		await _repository.DeleteAsync(cuePoint);
	}

	public async Task<CuePoint> GetCuePoint(long id)
	{
		return await _repository.GetAsync(id);
	}

	public async Task<CuePoint> UpdateCuePoint(CuePoint cuePoint)
	{
		return await _repository.UpdateAsync(cuePoint);
	}

	public CuePointsDto GetAllCuePointsFromRoute(long routeId)
	{
		var cuePoints = _repository.Items.Where(x => x.RouteId == routeId).Select(x => CuePointDto.FromEntity(x)).ToList() ?? [];
		return new(cuePoints);
	}

	public void CreateOrUpdateRange(CuePointsDto dto)
	{

	}
}
