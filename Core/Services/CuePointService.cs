using Core.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class CuePointService(IRepository<CuePoint> repository) : ICuePointService
{
	public IEnumerable<CuePointDto> GetAllCuePointsFromRoute(long routeId)
	{
		return repository
			.Items
			.Include(x => x.Attachment)
			.Where(x => x.RouteId == routeId)
			.Select(x => CuePointDto.FromEntity(x))
			.ToList() ?? [];
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