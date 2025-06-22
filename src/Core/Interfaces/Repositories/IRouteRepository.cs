using Infrastructure.Entities;

namespace Core.Interfaces.Repositories;

public interface IRouteRepository : IRepository<Route>
{	
	/// <summary>
	/// Обновляет Маршрут и связи между категориями
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	Task<Route> UpdateRoute(Route entity);
}
