using Core.Dto.CuePoint;
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

	public async Task<IEnumerable<CuePointDto>> UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto, long routeId)
	{
		var normalizedDtos = NormalizeSortedIndexes(dto).ToList();

		var forCreate = normalizedDtos.Where(x => x.Id is null).ToList();
		var forUpdate = normalizedDtos.Where(x => x.Id is not null).ToList();

		// Получаем все существующие точки для этого маршрута
		var existingEntities = await repository.Items.Where(x => x.RouteId == routeId).ToListAsync();

		// Идентификаторы, которые пришли во входящих данных
		var incomingIds = forUpdate.Select(x => x.Id!.Value).ToHashSet();

		// Удаляем те, которых нет во входящих
		var toDeleteIds = existingEntities
			.Where(e => !incomingIds.Contains(e.Id.Value))
			.Select(e => e.Id.Value)
			.ToList();
		if (toDeleteIds.Any())
		{
			await repository.DeleteRangeAsync(toDeleteIds);
		}

		// Обновляем существующие
		await repository.UpdateRangeAsync(forUpdate.Select(CuePointDto.ToEntity));

		// Создаём новые
		var createdEntities = await repository.CreateRangeAsync(forCreate.Select(CuePointDto.ToEntity));
		var createdDtos = createdEntities.Select(CuePointDto.FromEntity);

		// Объединяем и сортируем
		var allDtos = forUpdate.Concat(createdDtos);

		return allDtos.OrderBy(x => x.SortIndex);
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