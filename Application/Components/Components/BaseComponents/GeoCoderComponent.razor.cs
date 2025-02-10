using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.BaseComponents;

public partial class GeoCoderComponent : ComponentBase
{
	private string address;
	public GeocodeResult? Result { get; set; }

	[Parameter]
	public EventCallback<GeocodeResult> ResultEvent { get; set; }
	private string errorMessage;
	private IJSObjectReference module { get; set; }
	
	[Parameter]
	public bool IsVisible { get; set; } = false;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			module = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/Components/BaseComponents/GeoCoderComponent.razor.js");
		}
	}

	private async Task GeocodeAddress()
	{
		if (!string.IsNullOrEmpty(address))
		{
			try
			{
				Result = await JS.InvokeAsync<GeocodeResult>("yandexGeocoder.geocodeAddress", address);
				errorMessage = string.Empty;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				Result = null;
			}
		}
		
		await ResultEvent.InvokeAsync(Result);
	}

	public void Show()
	{
		IsVisible = true;
		StateHasChanged();
	}

	public void Hide()
	{
		IsVisible = false;
		StateHasChanged();
	}

	private void Close()
	{
		Hide();
	}
}

public class GeocodeResult
{
	public double Latitude { get; set; }
	public double Longitude { get; set; }
}
