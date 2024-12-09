using Core.Dto;
using Microsoft.AspNetCore.Components;

namespace Application.Components;

public partial class NewCuePointComponent
{
	public NewCuePointDto CuePoint { get; set; } = new();

	public bool IsHidden { get; set; } = true;

	[Parameter]
	public EventCallback<NewCuePointDto> OnSave { get; set; }

	public async Task HandleSave()
	{
		await OnSave.InvokeAsync(CuePoint);
	}

	public void Show()
	{
		IsHidden = false;
	}
	public void Hide()
	{
		IsHidden = true;
	}
}