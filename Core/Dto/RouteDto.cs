using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Core.Interfaces.Entities;
using Mapster;

namespace Core.Dto;

public class RouteDto : IFilteredRoute
{
	public static readonly string URI_PATTERN_ADMIN = "admin/route/{0}";

	public RouteDto(
		long id,
		string title,
		string description,
		int maxCountPeople,
		int minCountPeople,
		float baseCost,
		DateTime creationDateTime,
		string routeTypes,
		bool isHidden)
	{
		Id = id;
		Title = title;
		Description = description;
		MaxCountPeople = maxCountPeople;
		MinCountPeople = minCountPeople;
		BaseCost = baseCost;
		CreationDateTime = creationDateTime;
		RouteTypes = routeTypes;
		IsHidden = isHidden;
	}

	#region
	public long Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public int MaxCountPeople { get; set; }
	public int MinCountPeople { get; set; }
	public float BaseCost { get; set; }
	public DateTime CreationDateTime { get; set; }
	public string RouteTypes { get; set; }
	public bool IsHidden { get; set; }
	#endregion

	public static Route ToEntity(RouteDto dto) =>
		new Route(id: dto.Id,
			title: dto.Title,
			description: dto.Description,
			maxCountPeople: dto.MaxCountPeople,
			minCountPeople: dto.MinCountPeople,
			baseCost: dto.BaseCost,
			creationDateTime: dto.CreationDateTime,
			routeTypes: dto.RouteTypes,
			isHidden: dto.IsHidden);

	public static RouteDto FromEntity(Route entity) =>
		new RouteDto(
			id: entity.Id,
			title: entity.Title,
			description: entity.Description,
			maxCountPeople: entity.MaxCountPeople,
			minCountPeople: entity.MinCountPeople,
			baseCost: entity.BaseCost,
			creationDateTime: entity.CreationDateTime,
			routeTypes: entity.RouteTypes,
			isHidden: entity.IsHidden);
}