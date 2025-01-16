using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Admin;

public partial class CreationNewRouteComponent : ComponentBase
{
	public required NewRouteDto NewRoute { get; set; }
	public required IEnumerable<PropertyInfo> FormFields { get; set; }
	bool IsHidden { get; set; } = false;

	protected override void OnInitialized()
	{
		NewRoute = new();
		FormFields = NewRoute.GetFormFields();
	}

	void Save(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
	{
		var a = NewRoute;
		var b = 0;
	}

	string GetDisplayName(PropertyInfo property)
	{
		var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
		return displayAttribute?.Name ?? property.Name;
	}

	void InputOnChange(ChangeEventArgs e, PropertyInfo propertyInfo)
	{
		var value = e?.Value?.ToString();
		object? resultValue = null;
		if (propertyInfo.PropertyType == typeof(string) && value is { })
		{
			resultValue = value;
		}
		else if (propertyInfo.PropertyType == typeof(int?) && value is { })
		{
			resultValue = int.Parse(value);
		}
		else if (propertyInfo.PropertyType == typeof(float?) && value is { })
		{
			resultValue = float.Parse(value);
		}

		propertyInfo.SetValue(NewRoute, resultValue);
	}
}


public class NewRouteDto
{
	[Display(Name = "Название")]
	public string? Title { get; set; }
	[Display(Name = "Описание")]
	public string? Description { get; set; }
	[Display(Name = "Максимальное количество людей")]
	public int? MaxCountPeople { get; set; }
	[Display(Name = "Минимальное количество людей")]
	public int? MinCountPeople { get; set; }
	[Display(Name = "Базова цена")]
	public float? BaseCost { get; set; }

	public IEnumerable<PropertyInfo> GetFormFields()
	{
		return GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
	}
}