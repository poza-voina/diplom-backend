using Infrastructure.Entities;

namespace Core.Interfaces.Repositories;

public interface IRouteRepository : IRepository<Route>
{
	Task<Route> UpdateRelationShips(Route entity);
}
