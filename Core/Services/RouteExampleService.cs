using Core.Dto;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;

namespace Core.Services;

public class RouteExampleService(IRepository<RouteExample> _repository) : IRouteExampleService
{
	public async Task<RouteExampleDto> CreateAsync(RouteExampleDto entity) =>
		await CreateAsync(RouteExampleDto.ToEntity(entity));

	public async Task<RouteExampleDto> CreateAsync(RouteExample entity) =>
		RouteExampleDto.FromEntity(await _repository.CreateAsync(entity));

	public async Task DeleteAsync(RouteExampleDto entity) =>
		await DeleteAsync(RouteExampleDto.ToEntity(entity));

	public async Task DeleteAsync(long id) =>
		await _repository.DeleteAsync(id);

	public async Task DeleteAsync(RouteExample entity) =>
		await _repository.DeleteAsync(entity);

	public async Task<RouteExampleDto> GetAsync(long id) =>
		RouteExampleDto.FromEntity(await _repository.GetAsync(id));

	public async Task<RouteExampleDto> UpdateAsync(RouteExampleDto entity) =>
		await UpdateAsync(RouteExampleDto.ToEntity(entity));

	public async Task<RouteExampleDto> UpdateAsync(RouteExample entity) =>
		RouteExampleDto.FromEntity(await _repository.UpdateAsync(entity));

	public async Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId) =>
		_repository.Items.Where(x => x.RouteId == routeId).Select(x => RouteExampleDto.FromEntity(x)).ToList();
}
