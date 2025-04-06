namespace Infrastructure.Exceptions;

[Serializable]
public class InfrastructureBaseException : Exception
{
	public InfrastructureBaseException()
	{
	}

	public InfrastructureBaseException(string? message) : base(message)
	{
	}

	public InfrastructureBaseException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
