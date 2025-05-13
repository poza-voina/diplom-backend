using Core.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;

namespace Core.Services;

public class CuePointService(IRepository<CuePoint> repository) : ICuePointService
{
	public IEnumerable<CuePointDto> GetAllCuePointsFromRoute(long routeId)
	{
		return repository
			.Items
			.Where(x => x.RouteId == routeId)
			.Select(x => CuePointDto.FromEntity(x))
			.ToList() ?? [];
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
		return CuePointDto.FromEntity(await repository.CreateAsync(entity));
	}

	public async Task<CuePointDto> UpdateAsync(CuePoint entity)
	{
		return CuePointDto.FromEntity(await repository.UpdateAsync(entity));
	}

	public async Task DeleteAsync(CuePoint entity)
	{
		await repository.DeleteAsync(entity);
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<CuePointDto> GetAsync(long id)
	{
		return CuePointDto.FromEntity(await repository.GetAsync(id));
	}

	public async Task UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto)
	{

		dto = NormalizeSortedIndexes(dto);

		var dtoForCreate = dto.Where(x => x.Id is null);
		var dtoForUpdate = dto.Where(x => x.Id is { });

		await repository.UpdateRangeAsync(dtoForUpdate.Select(x => CuePointDto.ToEntity(x)));
		await repository.CreateRangeAsync(dtoForCreate.Select(x => CuePointDto.ToEntity(x)));
	}

	private IEnumerable<CuePointDto> NormalizeSortedIndexes(IEnumerable<CuePointDto> dto)
	{
		int currentIndex = 0;
		var orderedCuePointDto = dto.OrderBy(x => x.SortIndex, new SortIndexComparer());
		foreach (var item in orderedCuePointDto)
		{
			item.SortIndex = currentIndex++;
		}
		return orderedCuePointDto;
	} 
}


public class SortIndexComparer : IComparer<int>
{
	public int Compare(int x, int y)
	{
		return x.CompareTo(y);
	}
}