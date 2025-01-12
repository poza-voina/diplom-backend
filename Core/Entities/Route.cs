using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Entities;

namespace Core.Entities;

public class Route : BaseEntity, IFilteredRoute
{
	public Route
		(
		long id,
		string title,
		string description,
		int maxCountPeople,
		int minCountPeople,
		float baseCost,
		DateTime creationDateTime,
		string routeTypes,
		bool isHidden
		) : base(id)
	{
		Title = title;
		Description = description;
		MaxCountPeople = maxCountPeople;
		MinCountPeople = minCountPeople;
		BaseCost = baseCost;
		CreationDateTime = creationDateTime;
		RouteTypes = routeTypes;
		IsHidden = isHidden;
	}

	[Column("Title")]
	public string Title { get; set; }

	[Column("Description")]
	public string Description { get; set; }

	[Column("MaxCountPeople")]
	public int MaxCountPeople { get; set; }

	[Column("MinCountPeople")]
	public int MinCountPeople { get; set; }

	[Column("BaseCost")]
	public float BaseCost { get; set; }

	[Column("CreationDateTime")]
	public DateTime CreationDateTime { get; set; }

	[Column("RouteTypes")]
	public string RouteTypes { get; set; }

	[Column("IsHidden")]
	public bool IsHidden { get; set; }

	public virtual ICollection<RouteExample>? RouteExamples { get; set; }
}