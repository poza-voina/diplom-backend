﻿namespace Core.Dto.Admin;

public class UpdateAdminRequest
{
	public required string Email { get; set; }

	public required string PhoneNumber { get; set; }

	public required string FirstName { get; set; }

	public required string SecondName { get; set; }

	public required string Patronymic { get; set; }
}