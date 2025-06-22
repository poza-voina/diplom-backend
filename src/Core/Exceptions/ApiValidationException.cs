using Core.Dto;

namespace Core.Exceptions;

public class ApiValidationException : ApiBaseException
{
    public ApiValidationException()
    {
    }

    public ApiValidationException(string? message) : base(message)
    {
    }

    public ApiValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public static void ThrowIfPagginateIncorrect(IPaggination paggination)
    {
        if (paggination.PageNumber is { } && paggination.PageSize is { })
        {
            if (paggination.PageNumber <= 0 && paggination.PageSize <= 0)
            {
                throw new ApiValidationException("PageNumber должен быть больше 0 и PageSize должен быть больше 0");
            }
        }
        else if (paggination.PageNumber is { } || paggination.PageSize is { })
        {
            throw new ApiValidationException("Должны быть введены PageNumber и PageSize");
        }
    }
}
