using Core.Dto.RouteCategory;
using System.Text.Json.Serialization;

namespace Core.Dto.Route;

public class UpdateRouteRequest
{
	[JsonPropertyName("id")]
	public long Id { get; set; }

	[JsonPropertyName("title")]
	public required string Title { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }

	[JsonPropertyName("maxCountPeople")]
	public int? MaxCountPeople { get; set; }

	[JsonPropertyName("minCountPeople")]
	public int? MinCountPeople { get; set; }

	[JsonPropertyName("baseCost")]
	public float? BaseCost { get; set; }

	[JsonPropertyName("isHidden")]
	public bool IsHidden { get; set; }

	[JsonPropertyName("routeCategories")]
	public IEnumerable<RouteCategoryDto> RouteCategories { get; set; } = [];
}