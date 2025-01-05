using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Entities;

namespace Core.Entities;

public class Route(long id,
	string title,
	string description,
	int maxCountPeople,
	int minCountPeople,
	float baseCost,
	DateTime creationDateTime,
	DateTime startDateTime,
	string routeTypes,
	bool isHidden) : BaseEntity(id), IFilteredRoute
{
	[Column("Title")]
	public string Title { get; set; } = title ?? throw new ArgumentNullException(nameof(title));

	[Column("Description")]
	public string Description { get; set; } = description ?? throw new ArgumentNullException(nameof(description));

	[Column("MaxCountPeople")]
	public int MaxCountPeople { get; set; } = maxCountPeople;

	[Column("MinCountPeople")]
	public int MinCountPeople { get; set; } = minCountPeople;

	[Column("BaseCost")]
	public float BaseCost { get; set; } = baseCost;

	[Column("CreationDateTime")]
	public DateTime CreationDateTime { get; set; } = creationDateTime;

	[Column("StartDateTime")]
	public DateTime StartDateTime { get; set; } = startDateTime;

	[Column("RouteTypes")]
	public string RouteTypes { get; set; } = routeTypes ?? throw new ArgumentNullException(nameof(routeTypes));

	[Column("IsHidden")]
	public bool IsHidden { get; set; } = isHidden;
}