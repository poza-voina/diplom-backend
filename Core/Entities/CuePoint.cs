using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CuePoint
		(
			long id,
			string title,
			string description,
			string cuePointType,
			DateTime creationDateTime,
			long routeId,
			int sortIndex
		) : BaseEntity(id)
{
	[Column("Title")]
	public string Title { get; set; } = title;

	[Column("Description")]
	public string Description { get; set; } = description;

	[Column("CuePointType")]
	public string CuePointType { get; set; } = cuePointType;

	[Column("CreationDateTime")]
	public DateTime CreationDateTime { get; set; } = creationDateTime;

	[Column("RouteId")]
	public long RouteId { get; set; } = routeId;

	[Column("SortIndex")]
	public int SortIndex { get; set; } = sortIndex;
}
