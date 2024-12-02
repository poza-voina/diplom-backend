using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CuePoint
{
	[Column("Title")]
	public string Title { get; set; }

	[Column("Description")]
	public string Description { get; set; }

	[Column("CuePointType")]
	public string CuePointType { get; set; }

	[Column("CreationDateTime")]
	public string CreationDateTime { get; set; }

	[Column("NextCuePointId")]
	public long NextCuePointId { get; set; }

	[Column("PreviousCuePointId")]
	public long PreviousCuePointId { get; set; }

	[Column("SortIndex")]
	public long SortIndex { get; set; }
}
