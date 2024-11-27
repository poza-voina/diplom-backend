using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Route
{
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
	
	[Column("StartDateTime")]
	public DateTime StartDateTime { get; set; }

	[Column("RouteTypes")]
	public string RouteTypes { get; set; } // ?

	[Column("CuePoint")]
	public string CuePoint { get; set; } // ?
}
