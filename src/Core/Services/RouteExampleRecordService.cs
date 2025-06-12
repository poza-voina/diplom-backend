using Core.Dto;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public interface IRouteExampleRecordService
{
	Task<IEnumerable<RouteExampleRecordWithClientDto>> FilterAsync(RouteExamplesRecordFilterRequest request);
}

public class RouteExampleRecordService(IRepository<RouteExampleRecord> repository) : IRouteExampleRecordService
{
	public async Task<IEnumerable<RouteExampleRecordWithClientDto>> FilterAsync(RouteExamplesRecordFilterRequest request)
	{
		var query = repository.Items.Include(x => x.Client).AsQueryable();

		if (request.PageNumber is { } && request.PageSize is { })
		{
			query = query.Paginate(request.PageNumber.Value, request.PageSize.Value);
		}
		else if (request.PageNumber is { } || request.PageSize is { })
		{
			throw new InvalidOperationException("Чтобы работала пагинация нужно вводить PageNumber и PageSize");
		}

		IEnumerable<RouteExampleRecord> result = await query.ToListAsync();

		return result.Adapt<IEnumerable<RouteExampleRecordWithClientDto>>();

	}
}

