using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Pages.Admin;

public partial class RoutePage : ComponentBase
{
	[Parameter]
	public required long RouteId { get; set; }

	[Inject]
	public required IRouteService RouteService { get; set; }

	public required RouteDto? Route { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Route = await RouteService.GetRouteAsync(RouteId);
	}
}