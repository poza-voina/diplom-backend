using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Application.Components.Components.BaseComponents;

public partial class CKEditor
{
	string _id;
	[Parameter]
	public string Id
	{
		get => _id ?? $"CKEditor_{_uid}";
		set => _id = value;
	}

	readonly string _uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			//await JSRuntime.InvokeVoidAsync("import","/ckeditor/ckeditor.js");
			//await JSRuntime.InvokeVoidAsync("import", "/ckeditorIntegration/js/ckeditorInterop.js");
			await JSRuntime.InvokeVoidAsync("CKEditorInterop.init", Id, DotNetObjectReference.Create(this));
		}

		await base.OnAfterRenderAsync(firstRender);
	}

	[JSInvokable]
	public Task EditorDataChanged(string data)
	{
		CurrentValue = data;
		StateHasChanged();
		return Task.CompletedTask;
	}

	protected override void Dispose(bool disposing)
	{
		JSRuntime.InvokeVoidAsync("CKEditorInterop.destroy", Id);
		base.Dispose(disposing);
	}
}