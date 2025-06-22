using Infrastructure.Enums;

namespace Core.Dto;

public class RouteExampleRecordDto
{
	public long Id { get; set; }
    public long ClientId { get; set; }
    public long RouteExampleId { get; set; }
    public RouteExampleRecordStatus Status { get; set; }
}

public class RouteExampleRecordWithRouteExampleDto : RouteExampleRecordDto
{
	public required RouteExampleWithRouteDto RouteExample {get;set;}
}

public class RouteExampleRecordWithClientDto : RouteExampleRecordWithRouteExampleDto
{
	public required ClientDto Client { get; set; }
}

public class ClientDto
{
	public required string Email { get; set; }

	public required string FirstName { get; set; }

	public required string SecondName { get; set; }

	public required string PhoneNumber { get; set; }
	
	public string? Patronymic { get; set; }
}