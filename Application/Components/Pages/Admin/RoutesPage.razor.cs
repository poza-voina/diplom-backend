using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Components.Pages.Admin;

public partial class RoutesPage : ComponentBase
{
	[Inject]
	private NavigationManager Navigation { get; set; } = default!;

	[Inject]
	IRouteService RouteService { get; init; } = default!;
	public RoutesDto Routes { get; set; } = default!;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Routes = await RouteService.GetRoutesPerPage(1, 10);
		}
		catch (Exception)
		{
			Routes = new();
		}
	}

	private void GoToRoute(long id)
	{
		Navigation.NavigateTo(string.Format(RouteDto.URI_PATTERN_ADMIN, id));

	}
	private void DeleteRoute()
	{

	}

	private async Task ShowRoute(RouteDto dto)
	{
		await RouteService.ShowRoute(dto);
	}

	private async Task HideRoute(RouteDto dto)
	{
		await RouteService.HideRoute(dto);
	}
}