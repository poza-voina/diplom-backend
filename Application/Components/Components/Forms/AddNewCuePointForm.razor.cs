using Core.Dto;
using Microsoft.AspNetCore.Components;

namespace Application.Components.Components.Forms;

public partial class AddNewCuePointForm : ComponentBase
{
	public NewCuePointDto CuePoint { get; set; } = new();

	public required FormField TitleField { get; set; }
	public required FormField DescriptionField { get; set; }
	public required FormField AddressField { get; set; }

	[Parameter]
	public EventCallback<NewCuePointDto> OnSave { get; set; }

	public async Task HandleSave()
	{
		await OnSave.InvokeAsync(CuePoint);
	}
}