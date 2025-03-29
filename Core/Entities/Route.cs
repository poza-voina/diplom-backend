using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Route : BaseEntity
{
	[Column("Title")]
	public required string Title { get; set; }

	[Column("Description")]
	public string? Description { get; set; }

	[Column("MaxCountPeople")]
	public int? MaxCountPeople { get; set; }

	[Column("MinCountPeople")]
	public int? MinCountPeople { get; set; }

	[Column("BaseCost")]
	public float? BaseCost { get; set; }

	[Column("CreationDateTime")]
	public required DateTime CreationDateTime { get; set; }

	[Column("IsHidden")]
	public required bool IsHidden { get; set; }

	public virtual ICollection<RouteExample>? RouteExamples { get; set; }
}