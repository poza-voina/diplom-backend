using Core.Dto.Admin;

namespace Core.Interfaces.Services;

public interface IAdminService
{
	Task<AdminResponse> CreateAsync(CreateAdminRequest request);
	Task<AdminResponse> UpdateAsync(UpdateAdminRequest request);
	Task<IEnumerable<AdminResponse>> GetAllAsync(GetAllAdminRequest request);
	Task<AdminResponse> GetAsync(long id);
	Task DeleteAsync(long id);
}
