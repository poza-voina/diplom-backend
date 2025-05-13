﻿namespace Core.Dto;

public class GetBooksRequest
{
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
}
