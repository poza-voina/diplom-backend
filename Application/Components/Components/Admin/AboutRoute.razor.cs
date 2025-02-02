using Application.Components.Components.BaseComponents;
using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.Admin;

public partial class AboutRoute : ComponentBase
{
	[Parameter]
	public required RouteDto Route { get; set; }

	[Inject]
	public required IRouteService RouteService { get; set; }

	public required Modal ModalWindow { get; set; }

	bool IsTitleEdited { get; set; } = false;

	private void EditRouteDescription()
	{
		ModalWindow.Show();
	}

	private async Task ToggleEditRouteTitle()
	{
		if (IsTitleEdited)
		{
			await RouteService.UpdateAsync(Route);
			StateHasChanged();
		}

		IsTitleEdited = !IsTitleEdited;
	}

	private async Task SaveDescription()
	{
		await RouteService.UpdateAsync(Route);
	}
}