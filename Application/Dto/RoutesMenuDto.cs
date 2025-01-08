using System.Collections.Immutable;

namespace Application.Dto;

public class RoutesMenuDto
{
	public bool IsShowHidden { get; set; } = true;
	public bool IsShowVisible { get; set; } = true;

	public SortingTypes SortingType { get; set; }
}


public enum SortingTypes
{
	ByTitle, ByCreationDate
}


public static class SortingTypesHelper
{

	public static ImmutableDictionary<SortingTypes, string> _messages { get; set; } = new KeyValuePair<SortingTypes, string>[]{
	new(SortingTypes.ByTitle, "По названию"),
	new(SortingTypes.ByCreationDate, "По дате создания"),
	}.ToImmutableDictionary();

	public static string GetMessage(this SortingTypes sortingType) =>
			_messages[sortingType];

}