using Core.Dto.Admin;

namespace Core.Interfaces.Services;

public interface IAdminService
{
	Task<AdminDto> CreateAsync(CreateAdminRequest request);
	Task<AdminDto> UpdateAsync(UpdateAdminRequest request);
	Task<IEnumerable<AdminDto>> GetAllAsync(GetAllAdminRequest request);
	Task<AdminDto> GetAsync(long id);
	Task DeleteAsync(long id);
}
