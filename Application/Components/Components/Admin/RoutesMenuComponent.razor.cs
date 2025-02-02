using Application.Components.Components.BaseComponents;
using Application.Dto;
using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.Admin;

public partial class RoutesMenuComponent : ComponentBase
{
	public RoutesMenuDto Values { get; set; } = new();

	[Inject]
	public IJSRuntime JS { get; set; }

	[Inject]
	public required IRouteService RouteService { get; set; }

	[Parameter]
	public EventCallback<RoutesMenuDto> OnSend { get; set; }

	[Parameter]
	public EventCallback<RouteDto> OnNewRouteCreated { get; set; }

	[Parameter]
	public EventCallback OnNewRouteNonCreated { get; set; }

	[Parameter]
	public EventCallback OnStartingCreationNewRoute { get; set; }

	public required Modal ModalWindow { get; set; }

	private async Task HandleShowVisible()
	{
		Values.IsShowVisible = !Values.IsShowVisible;
		await Send();
	}
	private async Task HandleShowHidden()
	{
		Values.IsShowHidden = !Values.IsShowHidden;
		await Send();
	}

	private async Task HandleAddNewRouteAsync()
	{
		await OnStartingCreationNewRoute.InvokeAsync();
	}

	
	private async Task SetSortingType(SortingTypes sortingType)
	{
		Values.SortingType = sortingType;
		await Send();
	}

	private async Task Send()
	{
		await OnSend.InvokeAsync(Values);
	}

	private IJSObjectReference module { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			module = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/Components/Admin/RoutesMenuComponent.razor.js");
		}
	}


	private void OpenModal()
	{
		ModalWindow?.Show();
	}

	private void ModalClosed()
	{
		Console.WriteLine("Модальное окно закрыто.");
	}

	private async Task SaveNewRoute(NewRouteDto dto)
	{
		ModalWindow?.Hide();
		var newRoute = await RouteService.CreateAsync(NewRouteDto.ToCoreDto(dto));
		if (newRoute is { })
		{
			await OnNewRouteCreated.InvokeAsync(newRoute);
		} else
		{
			await OnNewRouteNonCreated.InvokeAsync(newRoute);
		}
	}


}
