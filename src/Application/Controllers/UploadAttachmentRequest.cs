namespace Application.Controllers;

public class UploadAttachmentRequest
{
	public long routeId { get; set; }
	public required IFormFile file { get; set; }
}