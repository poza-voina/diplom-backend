using Application.Dto;
using Core.Dto;
using Core.Functions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Core.Functions;
using Entities = Core.Entities;
using Application.Components.Components.Admin;
using Humanizer;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Application.Components.Components.BaseComponents;

namespace Application.Components.Pages.Admin;

public partial class RoutesPage : ComponentBase
{
	[Inject]
	private NavigationManager Navigation { get; set; } = default!;

	[Inject]
	IRouteService RouteService { get; init; } = default!;
	public RoutesDto Routes { get; set; } = default!;
	RoutesMenuComponent RouteMenuComponentExample { get; set; } = default!;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Routes = new RoutesDto((await RouteService.GetRoutesPerPage(1, 10)).ToList());
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
		await UpdateDto((IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>)Routes);
	}
	private async Task HideRoute(RouteDto dto)
	{
		await RouteService.HideRoute(dto);
		await UpdateDto((IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>)Routes);
	}

	private async Task UpdateDto(IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute> example)
	{
		var settings = RouteMenuComponentExample.Values;

		List<Func<IQueryable<IFilteredRoute>, IQueryable<IFilteredRoute>>> funcs = new();
		if (settings.IsShowVisible && !settings.IsShowHidden)
		{
			funcs.Add(AdminRoutesFunctions.GetVisibleRoutes);
		}
		if (settings.IsShowHidden && !settings.IsShowVisible)
		{
			funcs.Add(AdminRoutesFunctions.GetHiddenRoutes);
		}

		var a = await example.GetFilteredValuesAsync(funcs);
		if (a is { })
		{
			Routes.Values = a.Cast<RouteDto>().ToList();
		}
		StateHasChanged();
	}

	public void ShowCreatedRoute(RouteDto dto)
	{
		Routes.Values.Insert(0, dto);
	}

	public async Task Apply()
	{
		await UpdateDto((IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>)RouteService);
	}
}