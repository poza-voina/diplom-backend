namespace Core.Exceptions;

[Serializable]
internal class ApiEntityNotFoundException : ApiBaseException
{
	public ApiEntityNotFoundException()
	{
	}

	public ApiEntityNotFoundException(string? message) : base(message)
	{
	}

	public ApiEntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
