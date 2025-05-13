using System.Text.Json.Serialization;
using Core.Dto.RouteCategory;
using Infrastructure.Entities;
using Mapster;

namespace Core.Dto;

public class RouteDto
{
	public long? Id { get; set; }
	public required string Title { get; set; }
	public string? Description { get; set; }
	public int? MaxCountPeople { get; set; }
	public float? BaseCost { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public bool IsHidden { get; set; }

	public Attachment? Attachment { get; set; }
	public IEnumerable<RouteCategoryDto> RouteCategories { get;set;} = [];
	
}