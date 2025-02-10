using Core.Dto;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.VisualBasic;

namespace Core.Services;

public class CuePointService(ICuePointRepository repository) : ICuePointService
{
	private ICuePointRepository _repository = repository;

	public CuePointsDto GetAllCuePointsFromRoute(long routeId)
	{
		var cuePoints = _repository.Items.Where(x => x.RouteId == routeId).Select(x => CuePointDto.FromEntity(x)).ToList() ?? [];
		return new(cuePoints);
	}

	public async Task<CuePointDto> CreateAsync(CuePointDto entity)
	{
		return await CreateAsync(CuePointDto.ToEntity(entity));
	}

	public async Task<CuePointDto> UpdateAsync(CuePointDto entity)
	{
		return await UpdateAsync(CuePointDto.ToEntity(entity));
	}

	public async Task DeleteAsync(CuePointDto entity)
	{
		await DeleteAsync(CuePointDto.ToEntity(entity));
	}

	public async Task<CuePointDto> CreateAsync(CuePoint entity)
	{
		return CuePointDto.FromEntity(await _repository.CreateAsync(entity));
	}

	public async Task<CuePointDto> UpdateAsync(CuePoint entity)
	{
		return CuePointDto.FromEntity(await _repository.UpdateAsync(entity));
	}

	public async Task DeleteAsync(CuePoint entity)
	{
		await _repository.DeleteAsync(entity);
	}

	public async Task DeleteAsync(long id)
	{
		await _repository.DeleteAsync(id);
	}

	public async Task<CuePointDto> GetAsync(long id)
	{
		return CuePointDto.FromEntity(await _repository.GetAsync(id));
	}

	public async Task UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto)
	{
		var dtoForCreate = dto.Where(x => x.Id is null);
		var dtoForUpdate = dto.Where(x => x.Id is { });

		await _repository.UpdateRange(dtoForUpdate.Select(x => CuePointDto.ToEntity(x)));
		await _repository.CreateRange(dtoForCreate.Select(x => CuePointDto.ToEntity(x)));
	}
}
