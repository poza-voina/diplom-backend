using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.BaseComponents;

public partial class Modal : ComponentBase
{
	[Parameter]
	public RenderFragment? ChildContent { get; set; }

	[Parameter]
	public EventCallback OnClose { get; set; }

	private bool IsVisible { get; set; }

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
		OnClose.InvokeAsync();
	}
}