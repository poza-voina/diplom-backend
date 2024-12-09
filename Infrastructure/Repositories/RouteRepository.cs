using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class RouteRepository : Repository<Route>, IRouteRepository
{
	public RouteRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}
}
