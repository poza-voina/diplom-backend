using Microsoft.AspNetCore.Components.Web;

namespace Core.Dto;

public class GetFilteredRoutesExamplesRequest
{
	public bool IsRouteExamplePending { get; set; }
	public bool IsUserPending { get; set; }
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
}
