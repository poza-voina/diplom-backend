using System.Linq;
using Core.Dto;
using Core.Dto.RouteExample;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Exceptions;
using Mapster;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class RouteExampleService(
    IRepository<RouteExample> routeExampleRepository,
    IDateTimeConverter dateTimeConverter,
    IRepository<RouteExampleRecord> routeExampleRecordRepository) : IRouteExampleService
{
    public async Task<IEnumerable<RouteExampleDto>> GetByMonthAsync(GetRoutesExampleFromMonthRequest request)
    {
        var startDate = new DateTime(request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var endDate = startDate.AddMonths(1);

        var recordsQuery = routeExampleRepository.Items
            .Include(x => x.RouteExampleRecords)
            .Where(x => x.StartDateTime >= startDate &&
                        x.StartDateTime < endDate &&
                        x.RouteId == request.RouteId && x.Status == RouteExampleStatus.Pending);


        var records = await recordsQuery.ToListAsync();

        var result = records.Adapt<List<RouteExampleDto>>();
        var lengths = records.Select(x => x.RouteExampleRecords.Count).ToList();

        for (var i = 0; i < records.Count; i++)
        {
            result[i].CountRecords = lengths[i];
        }

        return result;
    }

    public async Task<RouteExampleDto> CreateAsync(RouteExampleDto dto)
    {
        var entity = RouteExampleDto.ToEntity(dto);
        var result = await routeExampleRepository.CreateAsync(entity);

        return RouteExampleDto.FromEntity(result);
    }

    public async Task DeleteAsync(long id) =>
        await routeExampleRepository.DeleteAsync(id);

    public async Task<RouteExampleDto> GetAsync(long id)
    {
        var entity = (await routeExampleRepository.Items.Include(x => x.RouteExampleRecords).FirstOrDefaultAsync(x => x.Id == id)) ?? throw new ApiEntityNotFoundException("не удалось найти");
        var dto = RouteExampleDto.FromEntity(entity);
        dto.CountRecords = entity.RouteExampleRecords.Count;

        return dto;
    }

    public async Task<RouteExampleDto> UpdateAsync(RouteExampleDto dto)
    {
        var entity = RouteExampleDto.ToEntity(dto);
        var result = await routeExampleRepository.UpdateAsync(entity);

        return RouteExampleDto.FromEntity(result);
    }

    public async Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId)
    {
        return await routeExampleRepository.Items
            .Where(x => x.RouteId == routeId)
            .Select(x => RouteExampleDto.FromEntity(x)).ToListAsync();
    }

    public async Task<RouteExampleDto> CreateOrUpdateAsync(RouteExampleCreateOrUpdateRequest request)
    {
        if (request.Id == 0)
        {
            request.Id = null;
        }
        request.StartDateTime = dateTimeConverter.ConvertToUtc(request.StartDateTime);
        request.EndDateTime = dateTimeConverter.ConvertToUtc(request.EndDateTime);
        var entity = request.Adapt<RouteExample>();

        RouteExample result;
        if (entity.Id is { } || entity.Id > 0)
        {
            result = await routeExampleRepository.UpdateAsync(entity);
        }
        else
        {
            result = await routeExampleRepository.CreateAsync(entity);
        }

        return result.Adapt<RouteExampleDto>();
    }

    public async Task<IEnumerable<RouteExampleDto>> CreateOrUpdateByRouteAsync(IEnumerable<RouteExampleCreateOrUpdateRequest> request)
    {
        foreach (var route in request)
        {
            route.StartDateTime = dateTimeConverter.ConvertToUtc(route.StartDateTime);
            route.EndDateTime = dateTimeConverter.ConvertToUtc(route.EndDateTime);
            if (route.Id == 0)
            {
                route.Id = null;
            }
        }

        var entities = request
            .Adapt<IEnumerable<RouteExample>>();

        var forUpdate = entities
            .Where(x => x.Id is { } || x.Id > 0);

        var forCreate = entities
            .Where(x => x.Id is null || x.Id <= 0);

        var updatingResult = await routeExampleRepository.UpdateRangeAsync(forUpdate);
        var creatingResult = await routeExampleRepository.CreateRangeAsync(forCreate);

        return updatingResult.Concat(creatingResult).Adapt<IEnumerable<RouteExampleDto>>();

    }

    public async Task<RouteExampleRecordWithRouteExampleDto> BookAsync(Client client, long routeExampleId)
    {
        var record = new RouteExampleRecord
        {
            ClientId = client.Id.Value,
            RouteExampleId = routeExampleId,
            Status = RouteExampleRecordStatus.Pending
        };

        await routeExampleRecordRepository.CreateAsync(record);

        return record.Adapt<RouteExampleRecordWithRouteExampleDto>();
    }

    public async Task<RouteExampleRecordWithRouteExampleDto> UnBookAsync(Client client, long routeExampleId)
    {
        var record = await routeExampleRecordRepository.Items.SingleOrDefaultAsync(x => x.ClientId == client.Id && x.RouteExampleId == routeExampleId) ?? throw new EntityNotFoundException("Пользователь не записывался на этот маршрут");
        if (record.Status == RouteExampleRecordStatus.Approved)
        {
            throw new InvalidOperationException("Нельзя отписываться от одобренного маршрута");
        }
        await routeExampleRecordRepository.DeleteAsync(record);

        return record.Adapt<RouteExampleRecordWithRouteExampleDto>();
    }

    public async Task<CollectionDto<RouteExampleWithRouteDto>> GetExamplesFilterAsync(GetFilteredRoutesExamplesRequest request)
    {
        var query = routeExampleRepository.Items.Include(x => x.RouteExampleRecords).Include(x => x.Route).AsQueryable();
        var totalsPages = 0;

        if (request.IsRouteExamplePending)
        {
            query = query.Where(x => x.Status == RouteExampleStatus.Pending);
        }
        if (request.IsUserPending)
        {
            query = query.Where(x => x.RouteExampleRecords.Count > 0 && x.RouteExampleRecords.Any(r => r.Status == RouteExampleRecordStatus.Pending));
        }
        if (request.PageNumber is { } && request.PageSize is { })
        {
            totalsPages = query.GetTotalsPages(request.PageSize.Value);
            query = query.Paginate(request.PageNumber.Value, request.PageSize.Value);
        }
        else if (request.PageNumber is { } || request.PageSize is { })
        {
            throw new ApiBaseException("При использовании пагинации нужно вводить PageNumber и PageSize");
        }

        var result = (await query.ToListAsync()).Adapt<RouteExampleWithRouteDto[]>();

        return new CollectionDto<RouteExampleWithRouteDto>
        {
            Values = result,
            TotalPages = totalsPages
        };
    }
}
