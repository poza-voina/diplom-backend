using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Forms;

public partial class AddNewRouteExample : ComponentBase
{
	RouteExampleDto RouteExample { get; set; } = new();

	[Parameter]
	public EventCallback<RouteExampleDto> OnSave { get; set; }

	[Inject]
	public required IRouteExampleService RouteExampleService { get; set; }

	public required FormField StartDateTimeFormField { get; set; }
	public required FormField EndDateTimeFormField { get; set; }

	private async Task Save()
	{
		if (Validate())
		{
			await OnSave.InvokeAsync(RouteExample);
		}
	}

	private bool Validate()
	{
		bool isValid = true;
		if (RouteExample.StartDateTime is null)
		{
			StartDateTimeFormField.SetErrors(["Дата начала не может быть пустой"]);
			isValid = false;
		}
		
		if (RouteExample.EndDateTime is null)
		{
			EndDateTimeFormField.SetErrors(["Дата окончания не может быть пустой"]);
			isValid = false;
		}

		return isValid;
	}
}