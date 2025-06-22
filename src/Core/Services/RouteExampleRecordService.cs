using Core.Dto;
using Core.Dto.RouteExampleRecord;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public interface IRouteExampleRecordService
{
    Task<RouteExampleRecordDto> ChangeStatusAsync(ChangeRecordsStatusRequest request);
    Task<IEnumerable<RouteExampleRecordDto>> ChangeStatusesAsync(IEnumerable<RouteExampleRecordDto> request);
    Task<IEnumerable<RouteExampleRecordWithClientDto>> FilterAsync(RouteExamplesRecordFilterRequest request);
}

public class RouteExampleRecordService(IRepository<RouteExampleRecord> repository) : IRouteExampleRecordService
{
    public async Task<RouteExampleRecordDto> ChangeStatusAsync(ChangeRecordsStatusRequest request)
    {
        var entity = request.Adapt<RouteExampleRecord>();
        var updated = await repository.UpdateAsync(entity);
        return updated.Adapt<RouteExampleRecordDto>();
    }

    public async Task<IEnumerable<RouteExampleRecordDto>> ChangeStatusesAsync(IEnumerable<RouteExampleRecordDto> request)
    {
        var entities = request.Adapt<IEnumerable<RouteExampleRecord>>();
        var updated = await repository.UpdateRangeAsync(entities);

        return updated.Adapt<IEnumerable<RouteExampleRecordDto>>();
    }

    public async Task<IEnumerable<RouteExampleRecordWithClientDto>> FilterAsync(RouteExamplesRecordFilterRequest request)
    {
        var query = repository.Items.Include(x => x.Client).AsQueryable();

        if (request.RouteExampleId.HasValue)
        {
            query = query.Where(x => x.RouteExampleId == request.RouteExampleId);
        }

        if (request.PageNumber is { } && request.PageSize is { })
        {
            query = query.Paginate(request.PageNumber.Value, request.PageSize.Value);
        }
        else if (request.PageNumber is { } || request.PageSize is { })
        {
            throw new InvalidOperationException("Чтобы работала пагинация нужно вводить PageNumber и PageSize");
        }

        var result = (await query.ToListAsync()).Adapt<IEnumerable<RouteExampleRecordWithClientDto>>();
        return result;
    }
}

