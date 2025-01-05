using Core.Dto;
using Core.Entities;
using Core.Interfaces.Entities;

namespace Core.Interfaces;

public interface IGettableFilteredRoutes<TInput, TResult> where TInput : IFilteredRoute where TResult : IFilteredRoute
{
	public Task<IEnumerable<TResult>?> GetFilteredValuesAsync(IEnumerable<Func<IQueryable<TInput>, IQueryable<TInput>>> funcs);
}
