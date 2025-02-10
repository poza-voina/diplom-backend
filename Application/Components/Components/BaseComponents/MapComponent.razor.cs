using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.BaseComponents;

public partial class MapComponent : ComponentBase
{

	[Inject]
	public IJSRuntime JS { get; set; }

	private IJSObjectReference module { get; set; }

	private IJSObjectReference _mapObject;

	[Parameter]
	public EventCallback OnInitialized { get; set; }

	[Parameter]
	public List<List<double>> MapPoints { get; set; } = new();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			module = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/Components/BaseComponents/MapComponent.razor.js");
			await LoadYM("268c38ba-239f-4d68-b429-57fcd29522bb", "map");
		}

		await OnInitialized.InvokeAsync();
	}

	private async Task LoadYM(string apiKey, string mapContainer)
	{
		_mapObject = await JS.InvokeAsync<IJSObjectReference>("createYandexMapObject", apiKey, mapContainer);
	}

	public async Task AddPointsToMap(IEnumerable<GeocodeResult> points) {
		await JS.InvokeVoidAsync("addPointsToMap", _mapObject, points);
	}

	public async Task AddPointToMap(GeocodeResult point)
	{
		await JS.InvokeVoidAsync("addPointToMap", _mapObject, point);
	}

}