namespace Core.Dto;

public interface IPaggination
{
    int? PageNumber { get; set; }
	int? PageSize { get; set; }
}
