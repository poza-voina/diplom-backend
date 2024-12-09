using Core.Dto;
using Core.Interfaces.Services;
using Core.Services;

namespace Application.Components.Pages;

public partial class AdminPage
{
	public CuePointsDto CuePoints { get; set; }

	public bool IsHidden { get; set; }
	public IOrderedEnumerable<CuePointDto> CuePointsTruePosition { get; set; }
	public CuePointDto CuePointDto { get; set; }

	public NewCuePointComponent newCuePointComponent;
	private readonly ICuePointService _cuePointService;

	public ToggleBuffer ToggleBuffer { get; set; } = new ToggleBuffer();

	public AdminPage(ICuePointService cuePointService)
	{
		IsHidden = false;
		_cuePointService = cuePointService;
		CuePoints = _cuePointService.GetAllCuePointsFromRoute(1);
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

	public void Hide()
	{
		IsHidden = true;
	}
	public void Show()
	{
		IsHidden = false;
	}

	public void GetNewCuePoint(NewCuePointDto dto)
	{
		var a = dto.MapToCuePointDto();
		var max = CuePoints.Values.Count >= 0 ? CuePoints.Values.Count : 0;
		a.SortIndex = max;
		CuePoints.Add(a);
		newCuePointComponent.Hide();
		Show();
	}

	public void HandleAddNewCuePoint()
	{
		Hide();
		newCuePointComponent.Show();
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

