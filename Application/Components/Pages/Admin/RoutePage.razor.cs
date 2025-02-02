using Application.Components.Components.BaseComponents;
using Application.Components.Components.Forms;
using Core.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Application.Components.Pages.Admin;

public partial class RoutePage : ComponentBase
{
	[Parameter]
	public required long RouteId { get; set; }

	[Inject]
	public required IRouteService RouteService { get; set; }

	[Inject]
	public required IRouteExampleService RouteExampleService { get; set; }

	public required RouteDto? Route { get; set; }

	public required List<RouteExampleDto> RouteExamples { get; set; }

	public required Modal ModalWindow { get; set; }

	public required AddNewRouteExample AddNewRouteExampleFormRef { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Route = await RouteService.GetAsync(RouteId);
		RouteExamples = (await RouteExampleService.GetExamplesByRouteId(RouteId)).ToList();
	}

	public void HandleOpenModal()
	{
		ModalWindow.Show();
	}

	public async Task GetRouteExample(RouteExampleDto dto)
	{
		dto.RouteId = RouteId;
		CastToUniversalTime(dto);
		dto.CreationDateTime = DateTime.UtcNow;
		await RouteExampleService.CreateAsync(dto);
	}

	public void CastToUniversalTime(RouteExampleDto dto)
	{
		dto.StartDateTime = dto.StartDateTime!.Value.ToUniversalTime();
		dto.EndDateTime = dto.EndDateTime!.Value.ToUniversalTime();
	}
	private Task EditRouteNameAndDescription()
	{

		throw new NotImplementedException();
	}
}