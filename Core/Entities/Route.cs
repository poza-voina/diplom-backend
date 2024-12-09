using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Route
		(
			long id,
			string title,
			string description,
			int maxCountPeople,
			int minCountPeople,
			float baseCost,
			DateTime creationDateTime,
			DateTime startDateTime,
			string routeTypes,
			string cuePoint
		) : BaseEntity(id)
{
	[Column("Title")]
	public string Title { get; set; } = title;

	[Column("Description")]
	public string Description { get; set; } = description;

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
	public string RouteTypes { get; set; } = routeTypes;

	[Column("CuePoint")]
	public string CuePoint { get; set; } = cuePoint;
}
