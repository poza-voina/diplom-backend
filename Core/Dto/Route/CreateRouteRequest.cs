using Core.Dto.RouteCategory;
using System.Text.Json.Serialization;

namespace Core.Dto.Route;

public class CreateRouteRequest
{
	[JsonPropertyName("title")]
	public required string Title { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("maxCountPeople")]
	public int? MaxCountPeople { get; set; }

	[JsonPropertyName("baseCost")]
	public float? BaseCost { get; set; }

	[JsonPropertyName("isHidden")]
	public bool IsHidden { get; set; }

	[JsonPropertyName("categories")]
	public IEnumerable<RouteCategoryDto> RouteCategories { get; set; } = [];
}
