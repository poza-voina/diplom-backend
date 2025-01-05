using Core.Entities;
using Core.Interfaces.Entities;

namespace Core.Functions;

public static class AdminRoutesFunctions
{
	public static Func<IQueryable<IFilteredRoute>, IQueryable<IFilteredRoute>> GetHiddenRoutes = (routes) => routes.Where(x => x.IsHidden);
	public static Func<IQueryable<IFilteredRoute>, IQueryable<IFilteredRoute>> GetVisibleRoutes = (routes) => routes.Where(x => !x.IsHidden);
}
