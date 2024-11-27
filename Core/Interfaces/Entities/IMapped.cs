namespace Core.Interfaces.Entities;

public interface IMapped<T>
{
	public T MapToDto();
}