namespace Infrastructure.Exceptions;

public class EntityAlReadyExists : InfrastructureBaseException
{
	public EntityAlReadyExists(string? message) : base(message)
	{
	}
}
