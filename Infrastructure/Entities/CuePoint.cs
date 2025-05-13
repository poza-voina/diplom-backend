using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Infrastructure.Entities;

public class CuePoint : BaseEntity
{
	public required string Title { get; set; }
	public string? Description { get; set; }
	public string? CuePointType { get; set; }
	public DateTime CreatedAt { get; set; }
	public required long RouteId { get; set; }
	public required int SortIndex { get; set; }
	public string? Address { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }

	public Attachment? Attachment { get; set; }

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
