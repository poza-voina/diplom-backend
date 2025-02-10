using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Application.Interfaces;
using Core.Dto;
using Mapster;
using Microsoft.AspNetCore.Connections;

namespace Application.Dto;

public class NewRouteDto : AbstractFormResultData
{
	[Display(Name = "Название")]
	public string? Title { get; set; }

	[Display(Name = "Описание")]
	public string? Description { get; set; }

	[Display(Name = "Максимальное количество людей")]
	public int? MaxCountPeople { get; set; }

	[Display(Name = "Минимальное количество людей")]
	public int? MinCountPeople { get; set; }

	[Display(Name = "Базовая цена")]
	public float? BaseCost { get; set; }

	public string? GetDisplayName(string propertyName)
	{
		var property = GetType().GetProperty(propertyName);

		if (property != null)
		{
			var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
			if (displayAttribute != null)
			{
				return displayAttribute.Name;
			}
		}

		return propertyName;
	}

	public override Errors Validate()
	{
		var errors = new Errors();
		if (string.IsNullOrWhiteSpace(Title))
		{
			errors.Add(nameof(Title), "Не может быть пустым");
		}
		if (string.IsNullOrWhiteSpace(Description))
		{
			errors.Add(nameof(Description), "Не может быть пустым");
		}
		if (MinCountPeople is null)
		{
			errors.Add(nameof(MinCountPeople), "Не может быть пустым");
		}
		if (MaxCountPeople is null)
		{
			errors.Add(nameof(MaxCountPeople), "Не может быть пустым");
		}
		if (MinCountPeople > MaxCountPeople)
		{
			errors.Add(nameof(MinCountPeople), "Максимальное количество людей не может быть меньше чем минимальное");
			errors.Add(nameof(MaxCountPeople), "Максимальное количество людей не может быть меньше чем минимальное");
		}
		if (BaseCost is null)
		{
			errors.Add(nameof(BaseCost), "Не может быть пустым");
		}

		return errors;
	}

	public static RouteDto ToCoreDto(NewRouteDto newRoute)
	{
		return new RouteDto
		{
			Title = newRoute.Title ?? throw new NullReferenceException("The value of 'newRoute.Title' should not be null"),
			Description = newRoute.Description ?? throw new NullReferenceException("The value of 'newRoute.Description' should not be null"),
			MaxCountPeople = newRoute.MaxCountPeople ?? throw new NullReferenceException("The value of 'newRoute.MaxCountPeople' should not be null"),
			MinCountPeople = newRoute.MinCountPeople ?? throw new NullReferenceException("The value of 'newRoute.MinCountPeople' should not be null"),
			BaseCost = newRoute.BaseCost ?? throw new NullReferenceException("The value of 'newRoute.BaseCost' should not be null")
		};
	}
}