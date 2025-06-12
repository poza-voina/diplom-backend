namespace Infrastructure.Exceptions;

public class EntityNotFoundException : InfrastructureBaseException
{
	public EntityNotFoundException(string? message) : base(message)
	{
	}
}
