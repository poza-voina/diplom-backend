using Core.Dto.RouteCategory;

namespace Core.Interfaces.Services;

public interface IRouteCategoryService
{
	Task<RouteCategoryDto> GetAsync(long id);
	Task<RouteCategoryDto> CreateAsync(CreateRouteCategoryRequest dto);
	Task<RouteCategoryDto> UpdateAsync(UpdateRouteCategoryRequest dto);
	Task DeleteAsync(long id);
	Task<IEnumerable<RouteCategoryDto>> FilterAsync(FilterRouteCategoryRequest dto);
}
