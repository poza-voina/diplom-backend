using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Forms;

public partial class AddNewRouteForm : ComponentBase
{
	public required NewRouteDto NewRoute { get; set; }

	public required Errors FormErrors { get; set; }

	[Parameter]
	public EventCallback<NewRouteDto> OnSave { get; set; }

	protected override void OnInitialized()
	{
		FormErrors = new();
		NewRoute = new NewRouteDto();
	}

	private async Task Save(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
	{
		FormErrors = new();
		FormErrors = NewRoute.Validate();
		if (!FormErrors.IsErrors)
		{
			await OnSave.InvokeAsync(NewRoute);
		}
	}
}