using Core.Interfaces.Repositories;

namespace Core.Dto.CuePoint;


public class CuePointsDto
{
	public List<CuePointDto> Values { get; set; }

	public CuePointsDto(List<CuePointDto> values)
	{
		Values = values;
		UpdateIndexes();
	}

	public void UpdateIndexes()
	{
		List<int> originalIndexes = Values.Select(x => x.SortIndex).ToList();

		Dictionary<int, int> indexMap = originalIndexes
			.Distinct()
			.OrderBy(x => x)
			.Select((num, index) => new { num, index })
			.ToDictionary(x => x.num, x => x.index);

		foreach (var item in Values)
		{
			item.SortIndex = indexMap[item.SortIndex];
		}
	}

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

	public IOrderedEnumerable<CuePointDto> GetCuePoints()
	{
		return Values.OrderBy(x => x.SortIndex);
	}

	public void UpdateIndex(int currentIndex, int newIndex)
	{
		if (currentIndex > newIndex)
		{
			foreach (var item in Values)
			{
				if (item.SortIndex > currentIndex && item.SortIndex <= newIndex)
				{
					item.SortIndex--;
				}
			}
		}
		else
		{
			foreach (var item in Values)
			{
				if (item.SortIndex < currentIndex && item.SortIndex >= newIndex)
				{
					item.SortIndex++;
				}
			}
		}
	}

	public void Add(CuePointDto dto)
	{
		dto.SortIndex = Values.Select(x => x.SortIndex).Max() + 1;
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
}