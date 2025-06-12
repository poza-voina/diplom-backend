using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Services;

public interface IMinioService
{
	Task<string> UploadFileAsync(IFormFile file);

	Task<List<string>> UploadFilesAsync(List<IFormFile> files);

}
