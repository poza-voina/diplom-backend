using Core.Dto.RouteCategory;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class RouteCategoryService(IRepository<RouteCategory> repository) : IRouteCategoryService
{
	public async Task<RouteCategoryDto> CreateAsync(CreateRouteCategoryRequest dto)
	{
		var result = await repository.CreateAsync(dto.Adapt<RouteCategory>());
		return result.Adapt<RouteCategoryDto>();
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<IEnumerable<RouteCategoryDto>> FilterAsync(FilterRouteCategoryRequest dto)
	{
		var result = await repository
			.Items
			.Include(x => x.Routes)
			.Paginate(dto.PageNumber, dto.CountPerPage)
			.ToListAsync();

		return result.Adapt<IEnumerable<RouteCategoryDto>>();
	}

	public async Task<RouteCategoryDto> GetAsync(long id)
	{
		var result = await repository
			.Items
			.FirstOrDefaultAsync(x => x.Id == id) ?? throw new EntityNotFoundException("Не удалось найти категорию");

		return result.Adapt<RouteCategoryDto>();
	}

	public async Task<RouteCategoryDto> UpdateAsync(UpdateRouteCategoryRequest dto)
	{
		var result = await repository.UpdateAsync(dto.Adapt<RouteCategory>());
		return result.Adapt<RouteCategoryDto>();
	}
}
