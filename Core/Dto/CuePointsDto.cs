namespace Core.Dto;


public class CuePointsDto(List<CuePointDto> values)
{
	public List<CuePointDto> Values { get; set; } = values;

	public void Move(int sortIndex, int newSortIndex)
	{
		if (sortIndex < newSortIndex)
		{
			MoveRight(sortIndex, newSortIndex);
		}
		else if (sortIndex > newSortIndex)
		{
			MoveLeft(sortIndex, newSortIndex);
		}

		Print();
	}

	private void MoveLeft(int sortIndex, int newSortIndex)
	{
		foreach (var item in Values)
		{
			if (item.SortIndex == sortIndex)
			{
				if (newSortIndex >= 0)
				{
					item.SortIndex = newSortIndex;
				}
			}
			else if (item.SortIndex < Values.Count - 1 && item.SortIndex >= newSortIndex && item.SortIndex <= sortIndex)
			{
				item.SortIndex++;
			}
		}
	}

	private void MoveRight(int sortIndex, int newSortIndex)
	{
		foreach (var item in Values)
		{
			if (item.SortIndex == sortIndex)
			{
				if (newSortIndex < Values.Count)
				{
					item.SortIndex = newSortIndex;
				}
			}
			else if (item.SortIndex >= 1 && item.SortIndex >= sortIndex && item.SortIndex <= newSortIndex)
			{
				item.SortIndex--;
			}
		}
	}
	public IOrderedEnumerable<CuePointDto> GetCuePoints() => Values.OrderBy(x => x.SortIndex);

	public void Add(CuePointDto dto)
	{
		Values.Add(dto);
	}

	public void RemoveBySortIndex(int sortIndex)
	{
		Values.RemoveAll(x => x.SortIndex == sortIndex);

		foreach (var item in Values)
		{
			if (item.SortIndex >= sortIndex)
			{
				item.SortIndex--;
			}
		}
	}

	//!warning
	public void Print()
	{
		Console.WriteLine("----------");
		foreach (var item in Values)
		{
			Console.WriteLine($"{item.Title} - {item.SortIndex}");
		}
	}

	public void UpdateIndexes()
	{
		int a = 0;
		Values.OrderBy(x => x.SortIndex).Select(x => x.SortIndex = a++);
	}
}