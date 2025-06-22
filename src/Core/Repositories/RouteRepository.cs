using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Minio.DataModel.Notification;

namespace Core.Repositories;

public class RouteRepository(ApplicationDbContext dbContext) : Repository<Route>(dbContext), IRouteRepository
{
    /// <inheritdoc/>
    public async Task<Route> UpdateRoute(Route entity)
    {
        await UpdateSimplePropertiesAsync(entity);
        await UpdateRouteCategoriesLinksAsync(entity);

        return entity;
    }

    private async Task<Route> UpdateSimplePropertiesAsync(Route entity)
    {
        if (entity.Id == null)
            throw new ArgumentException("Id не может быть null");

        var existingRoute = await DbContext.Routes.FindAsync(entity.Id);
        if (existingRoute == null)
            throw new InvalidOperationException("Маршрут не найден в базе");

        var entry = DbContext.Entry(existingRoute);

        var simpleProperties = entry.Metadata.GetProperties()
            .Where(p => !p.IsPrimaryKey() && !p.IsForeignKey() && !p.IsShadowProperty())
            .ToList();

        foreach (var property in simpleProperties)
        {
            var propInfo = typeof(Route).GetProperty(property.Name);
            if (propInfo == null) continue;

            var newValue = propInfo.GetValue(entity);
            entry.Property(property.Name).CurrentValue = newValue;
            entry.Property(property.Name).IsModified = true;
        }

        await DbContext.SaveChangesAsync();

        return existingRoute;
    }

    private async Task UpdateRouteCategoriesLinksAsync(Route entity)
    {
        if (entity.Id == null)
            throw new ArgumentException("Id не может быть null");

        var existingCategories = DbContext.RouteRouteCategories
            .Where(x => x.RouteId == entity.Id);

        DbContext.RouteRouteCategories.RemoveRange(existingCategories);

        var newCategories = entity.RouteCategories
            .Select(x => new RouteRouteCategory
            {
                RouteId = entity.Id.Value,
                RouteCategoryId = x.Id!.Value
            });

        DbContext.RouteRouteCategories.AddRange(newCategories);

        await DbContext.SaveChangesAsync();
    }
}
