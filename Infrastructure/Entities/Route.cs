using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Route : BaseEntity
{
	public required string Title { get; set; }
	public string? Description { get; set; }
	public int? MaxCountPeople { get; set; }
	public int? MinCountPeople { get; set; }
	public float? BaseCost { get; set; }
	public required DateTime CreatedAt { get; set; }
	public required bool IsHidden { get; set; }

	public virtual ICollection<RouteCategory> RouteCategories { get; set; } = [];
	public virtual ICollection<RouteExample> RouteExamples { get; set; } = [];
}