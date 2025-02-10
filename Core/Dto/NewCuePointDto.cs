namespace Core.Dto;

public class NewCuePointDto
{
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Address { get; set; }

	public CuePointDto MapToCuePointDto()
	{
		return new CuePointDto()
		{
			Title = Title,
			Description = Description,
		};
	}
}
