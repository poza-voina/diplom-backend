using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public interface IDateTimeConverter
{
	DateTime ConvertFromUtc(DateTime utcDateTime);
	DateTime ConvertToUtc(DateTime dateTime);
}

public class DateTimeConverter : IDateTimeConverter
{
	private readonly string _timeZoneId;

	public DateTimeConverter(IConfiguration configuration)
	{
		// Получаем временную зону из конфигурации
		_timeZoneId = configuration.GetSection("TimeZoneId")?.Value 
			?? throw new ArgumentNullException("TimeZoneId", "TimeZoneId не может быть null.");
	}

	public DateTime ConvertFromUtc(DateTime utcDateTime)
	{
		TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId);
		return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
	}

    public DateTime ConvertToUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            // Уже в UTC — вернуть как есть
            return dateTime;
        }

        if (dateTime.Kind == DateTimeKind.Local)
        {
            // Можно конвертировать напрямую в UTC
            return dateTime.ToUniversalTime();
        }

        // Если Unspecified — считаем, что это время в часовом поясе _timeZoneId
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId);
        return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
    }
}
