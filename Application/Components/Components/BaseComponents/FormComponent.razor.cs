using Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Application.Components.Components.BaseComponents;

public partial class FormComponent<TResult> : ComponentBase where TResult : AbstractFormResultData, new()
{
	[Parameter]
	public EventCallback<TResult> OnSave { get; set; }

	[Parameter]
	public bool IsHidden { get; set; } = false;

	public required TResult ResultData { get; set; }

	public required FormFields Fields { get; set; }

	protected override void OnInitialized()
	{
		ResultData = new TResult();

		Fields = new FormFields(ResultData.GetFormFields()
			.Select(x => new FormField(x)));
		var test = 0;
	}

	async Task Save(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
	{
		var errors = ResultData.Validate();
		var isErrors = errors.IsErrors;

		if (isErrors) {
			Fields.MatchErrorsWithFields(errors);
		} else
		{
			await OnSave.InvokeAsync(ResultData);
		}
		StateHasChanged();
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
		try
		{
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
		}
		catch (Exception) { }

		propertyInfo.SetValue(ResultData, resultValue);

		var modelValue = propertyInfo.GetValue(ResultData);

		StateHasChanged();
	}
}

public class FormField
{
	public PropertyInfo Info { get; set; }
	public IEnumerable<string> ErrorMessages { get; set; }

	public FormField(PropertyInfo info)
	{
		Info = info;
		ErrorMessages = new List<string>();
	}
}


public class FormFields
{
	public IList<FormField> Value { get; set; }
	
	public FormFields(IEnumerable<FormField> value)
	{
		Value = value.ToList();
	}

	public IEnumerable<PropertyInfo> Properties
	{
		get
		{
			return Value.Select(x => x.Info).AsEnumerable();
		}
	}

	public IEnumerable<string> GetMessageByPropertyName(string propertyName)
	{
		return Value.First(x => x.Info.Name == propertyName).ErrorMessages;
	}

	public void MatchErrorsWithFields(Errors errors)
	{
		for (int i = 0; i < Value.Count; i++) {
			string propertyName = Value[i].Info.Name;
			if (errors.Value.Keys.Any(x => x == propertyName))
			{
				Value[i].ErrorMessages = errors.Value[Value[i].Info.Name];
			}
		}
	}
}