using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Entities;

namespace Core.Entities;

public class Route : BaseEntity, IFilteredRoute
{
	[Column("Title")]
	public required string Title { get; set; }

	[Column("Description")]
	public required string Description { get; set; }

	[Column("MaxCountPeople")]
	public required int MaxCountPeople { get; set; }

	[Column("MinCountPeople")]
	public required int MinCountPeople { get; set; }

	[Column("BaseCost")]
	public required float BaseCost { get; set; }

	[Column("CreationDateTime")]
	public required DateTime CreationDateTime { get; set; }

	[Column("RouteTypes")]
	public required string RouteTypes { get; set; }

	[Column("IsHidden")]
	public required bool IsHidden { get; set; }

	public virtual ICollection<RouteExample>? RouteExamples { get; set; }
}