using Application.Components.Components.BaseComponents;
using Core.Dto;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Application.Components.Pages.Admin;

public partial class MapRoutePage : ComponentBase
{
	[Parameter]
	public long? RouteId { get; set; }

	[Inject]
	public required ICuePointService CuePointService { get; init; }

	public required Modal ModalWindow { get; set; }

	public required GeoCoderComponent GeoCoderMenu { get; set; }

	public required MapComponent Map { get; set; }

	public required CuePointsDto CuePoints { get; set; }
	public bool IsHidden { get; set; }
	public IOrderedEnumerable<CuePointDto> CuePointsTruePosition { get; set; }
	public CuePointDto CuePointDto { get; set; }
	public NewCuePointComponent newCuePointComponent;


	public ToggleBuffer ToggleBuffer { get; set; } = new ToggleBuffer();

	protected override void OnInitialized()
	{
		IsHidden = false;
		CuePoints = CuePointService.GetAllCuePointsFromRoute(RouteId ?? throw new InvalidOperationException());
		UpdateCuePointsPossition();
	}

	public void AddCuePoint(CuePointDto cuePoint)
	{
		CuePoints.Add(cuePoint);
	}

	public void MoveLower(int index)
	{
		CuePoints.Move(index, index + 1);
		ToggleBuffer.Update(index, index + 1);
	}

	public void MoveHigher(int index)
	{
		CuePoints.Move(index, index - 1);
		ToggleBuffer.Update(index, index - 1);
	}

	public void UpdateCuePointsPossition()
	{
		CuePointsTruePosition = CuePoints.GetCuePoints();
	}

	public void Remove(int index)
	{
		CuePoints.RemoveBySortIndex(index);
		ToggleBuffer.UpdateAfterDelete(index);
	}

	public void Expand(int index)
	{
		if (ToggleBuffer.IsOn(index))
		{
			ToggleBuffer.Remove(index);
		}
		else
		{
			ToggleBuffer.Add(index);
		}
	}

	public void GetNewCuePoint(NewCuePointDto dto)
	{
		var a = dto.MapToCuePointDto();
		var max = CuePoints.Values.Count >= 0 ? CuePoints.Values.Count : 0;
		a.SortIndex = max;
		CuePoints.Add(a);
		ModalWindow.Hide();
	}

	public void HandleShowModalCreateNewCuePoint()
	{
		ModalWindow.Show();
	}

	private void HandleShowMenuForAddingPointToMap()
	{
		GeoCoderMenu.Show();
	}

	public async Task SetPointToMap(GeocodeResult? geocodeResult, CuePointDto? cuePoint)
	{
		if (geocodeResult is { } && cuePoint is { })
		{
			await Map.AddPointToMap(geocodeResult);
			cuePoint.Latitude = geocodeResult.Latitude;
			cuePoint.Longitude = geocodeResult.Longitude;
		}
	}

	public async Task UpdateMap()
	{
		await Map.AddPointsToMap(
			CuePoints
				.Values
				.Where(x => x.Latitude is { } && x.Longitude is { })
				.Select(
					x => new GeocodeResult 
						{ 
							Longitude = x.Longitude!.Value, 
							Latitude = x.Latitude!.Value
						}));
	}

	public async Task HandleSave()
	{
		foreach (var item in CuePoints.Values)
		{
			item.RouteId = RouteId;
		}

		await CuePointService.UpdateOrCreateRangeAsync(CuePoints.Values);
	}
}


public class ToggleBuffer
{
	public const int MAX_TOGGLES = 2;

	private int currentToggle = 0;

	private List<int> Values { get; set; } = new List<int>();

	public ToggleBuffer() { }

	public void Add(int index)
	{
		Values.Add(index);
		if (Values.Count > MAX_TOGGLES)
		{
			Values.RemoveAt(0);
		}
	}

	public void Remove(int index)
	{
		Values.RemoveAll(x => x == index);
	}

	public bool IsOn(int index)
	{
		return Values.Contains(index);
		return true;
	}

	public void Update(int index, int newIndex)
	{
		for (var i = 0; i < Values.Count; i++)
		{
			if (Values[i] == index)
			{
				Values[i] = newIndex;
			}
		}
	}

	public void UpdateAfterDelete(int index)
	{
		Values.RemoveAll(x => x == index);
	}

	public void AddCuePoint()
	{

	}

}