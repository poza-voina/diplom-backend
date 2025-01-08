namespace Core.Interfaces.Entities;

public interface IFilteredRoute
{
	public bool IsHidden { get; set; }
	public string Title { get; set; }
}