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
}
