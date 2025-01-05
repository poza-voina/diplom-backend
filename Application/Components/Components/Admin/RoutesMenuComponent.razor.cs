using Application.Dto;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Admin;

public partial class RoutesMenuComponent : ComponentBase
{
	public RoutesMenuDto Values { get; set; } = new();

	[Parameter]
	public EventCallback<RoutesMenuDto> OnSend { get; set; }

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

	private async Task Send()
	{
		await OnSend.InvokeAsync(Values);
	}
}
