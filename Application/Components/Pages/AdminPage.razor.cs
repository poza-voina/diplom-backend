using System.Data;
using System.Net;
using Core.Dto;

namespace Application.Components.Pages;

public partial class AdminPage
{
	public CuePointsDto CuePoints { get; set; } = new()
	{
		Values = new List<CuePointDto>
					{
						new CuePointDto() { Title = "a", SortIndex = 0},
						new CuePointDto() { Title = "b", SortIndex = 1},
						new CuePointDto() { Title = "c", SortIndex = 2},
						new CuePointDto() { Title = "d", SortIndex = 3},
						new CuePointDto() { Title = "e", SortIndex = 4},
					}
	};
	public IOrderedEnumerable<CuePointDto> CuePointsTruePosition { get; set; }

	public ToggleBuffer ToggleBuffer { get; set; } = new ToggleBuffer();

	public AdminPage()
	{
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

}

