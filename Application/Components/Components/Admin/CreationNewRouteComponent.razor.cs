using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Admin;

public class CreationNewRouteComponent : ComponentBase
{
	NewRouteDto NewRoute { get; set; } = new();
}



public class NewRouteDto
{
	[Display(Name="Название")]
	public string? Title { get; set; }
	public string? Description { get; set; }
	public int? MaxCountPeople { get; set; }
	public int? MinCountPeople { get; set; }
	public float? BaseCost { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public string? RouteTypes { get; set; }
	public bool? IsHidden { get; set; }


	public IEnumerable<(string FieldName, Type FieldType)> GetFormFields()
	{
		return GetType()
			.GetProperties()
			.Select(x => (x.Name, x.PropertyType))
			.AsEnumerable();
	}
}
