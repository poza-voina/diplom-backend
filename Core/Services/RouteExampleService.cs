using Core.Dto;
using Core.Dto.RouteCategory.RouteExample;
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
	IRepository<RouteExample> _repository,
	IDateTimeConverter dateTimeConverter,
	IRepository<RouteExampleRecord> routeExampleRecordRepository) : IRouteExampleService
{
	public async Task<IEnumerable<RouteExampleDto>> GetByMonthAsync(GetRoutesExampleFromMonthRequest request)
	{
		var records = await _repository.Items.Include(x => x.RouteExampleRecords)
			.Where(x => x.CreatedAt.Month == request.Month && x.CreatedAt.Year == request.Year && x.RouteId == request.RouteId)
			.ToListAsync();

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
		var result = await _repository.CreateAsync(entity);

		return RouteExampleDto.FromEntity(result);
	}

	public async Task DeleteAsync(long id) =>
		await _repository.DeleteAsync(id);

	public async Task<RouteExampleDto> GetAsync(long id) =>
		RouteExampleDto.FromEntity(await _repository.GetAsync(id));

	public async Task<RouteExampleDto> UpdateAsync(RouteExampleDto dto)
	{
		var entity = RouteExampleDto.ToEntity(dto);
		var result = await _repository.UpdateAsync(entity);
	
		return RouteExampleDto.FromEntity(result);
	}

	public async Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId)
	{
		return await _repository.Items
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
			result = await _repository.UpdateAsync(entity);
		}
		else
		{
			result = await _repository.CreateAsync(entity);
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

		var updatingResult = await _repository.UpdateRangeAsync(forUpdate);
		var creatingResult = await _repository.CreateRangeAsync(forCreate);

		return updatingResult.Concat(creatingResult).Adapt<IEnumerable<RouteExampleDto>>();

	}

	public async Task BookAsync(Client client, long routeExampleId) {
		var record = new RouteExampleRecord
		{
			ClientId = client.Id.Value,
			RouteExampleId = routeExampleId,
			Status = RouteExampleRecordStatus.Pending
		};

		await routeExampleRecordRepository.CreateAsync(record);
	}

	public async Task UnBookAsync(Client client, long routeExampleId)
	{
		var record = await routeExampleRecordRepository.Items.SingleOrDefaultAsync(x => x.ClientId == client.Id && x.RouteExampleId == routeExampleId) ?? throw new EntityNotFoundException("Пользователь не записывался на этот маршрут");
		if (record.Status == RouteExampleRecordStatus.Approved)
		{
			throw new InvalidOperationException("Нельзя отписываться от одобренного маршрута");
		}
		await routeExampleRecordRepository.DeleteAsync(record);
	}
}
