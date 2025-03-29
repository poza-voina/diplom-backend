using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Core.Entities;

public class CuePoint : BaseEntity
{
	[Column("Title")]
	public required string Title { get; set; }

	[Column("Description")]
	public string? Description { get; set; }

	[Column("CuePointType")]
	public string? CuePointType { get; set; }

	[Column("CreationDateTime")]
	public DateTime? CreationDateTime { get; set; }

	[Column("RouteId")]
	public required long RouteId { get; set; }

	[Column("SortIndex")]
	public required int SortIndex { get; set; }

	[Column("Address")]
	public string? Address { get; set; }

	[Column("Latitude")]
	public double? Latitude { get; set; }

	[Column("Longitude")]
	public double? Longitude { get; set; }

	public Tuple<double, double>? GetLocation()
	{
		if (Latitude is null || Longitude is null)
		{
			return null;
		}
		return new Tuple<double, double>( Latitude.Value, Longitude.Value );
	}

	public void SetLocation(Tuple<double, double> location)
	{
		Latitude = location.Item1;
		Longitude = location.Item2;
	}
}
