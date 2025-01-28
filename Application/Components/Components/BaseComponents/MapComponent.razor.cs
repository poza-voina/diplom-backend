using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.BaseComponents;

public partial class MapComponent : ComponentBase
{

	[Inject]
	public IJSRuntime JS { get; set; }

	private IJSObjectReference module { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			module = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/Components/BaseComponents/MapComponent.razor.js");
			await LoadYM("268c38ba-239f-4d68-b429-57fcd29522bb");
			await InitializeMap("map");
		}
	}

	private async Task LoadYM(string apikey)
	{
		await module.InvokeVoidAsync("loadYandexMaps", apikey);
	}

	private async Task InitializeMap(string containerId)
	{
		await module.InvokeVoidAsync("initializeMap", containerId);
	}
}