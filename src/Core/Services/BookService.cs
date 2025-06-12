using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dto;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public interface IBookService
{
	Task<IEnumerable<RouteExampleRecordDto>> GetBooksAsync(Client client, GetBooksRequest request);
	Task<RouteExampleRecordDto> GetBookAsync(Client client, long bookId);
}

public class BookService(IRepository<RouteExampleRecord> routeExampleRecordRepository, IDateTimeConverter dateTimeConverter) : IBookService
{
	public async Task<IEnumerable<RouteExampleRecordDto>> GetBooksAsync(Client client, GetBooksRequest request)
	{
		var recordsQuery = routeExampleRecordRepository
			.Items
			.Include(x => x.RouteExample)
			.ThenInclude(y => y.Route).AsQueryable();


		if (request.StartDate is { } && request.EndDate is { })
		{
			var startDateTime = dateTimeConverter.ConvertFromUtc(request.StartDate.Value);
			startDateTime = DateTime.SpecifyKind(startDateTime, DateTimeKind.Utc);
			var endDateTime = dateTimeConverter.ConvertFromUtc(request.EndDate.Value);
			endDateTime = DateTime.SpecifyKind(endDateTime, DateTimeKind.Utc);

			recordsQuery = recordsQuery.Where(
				x => x.RouteExample != null &&
				x.RouteExample.StartDateTime.Date >= startDateTime.Date &&
				x.RouteExample.StartDateTime.Date <= endDateTime.Date &&
				x.ClientId == client.Id);
		}
		else if (request.EndDate is { } || request.StartDate is { })
		{
			throw new InvalidOperationException("Неправильный фильтр даты");
		}

		if (request.PageNumber is { } && request.PageSize is { })
		{
			recordsQuery.Paginate(request.PageNumber.Value, request.PageSize.Value);
		}
		else if (request.PageNumber is { } || request.PageSize is { })
		{
			throw new InvalidOperationException("Неправильный фильтр пагинации");
		}

		var records = await recordsQuery.ToListAsync();

		return records.Adapt<IEnumerable<RouteExampleRecordDto>>();
	}

	public async Task<RouteExampleRecordDto> GetBookAsync(Client client, long bookId)
	{
		var record = await routeExampleRecordRepository
			.Items
			.Include(x => x.RouteExample)
			.ThenInclude(y => y.Route)
			.SingleOrDefaultAsync(x => x.Id == bookId && x.ClientId == client.Id)
			?? throw new InvalidOperationException("Не удалось найти бронь для клиента");

		return record.Adapt<RouteExampleRecordDto>();
	}
}
