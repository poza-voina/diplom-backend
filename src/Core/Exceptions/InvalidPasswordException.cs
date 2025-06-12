namespace Core.Exceptions;

[Serializable]
public class InvalidPasswordException : ApiBaseException
{
	public InvalidPasswordException()
	{
	}

	public InvalidPasswordException(string? message) : base(message)
	{
	}

	public InvalidPasswordException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
