using Core.Dto.Admin;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class AdminService(IRepository<Manager> repository) : IAdminService
{
	public async Task<AdminDto> CreateAsync(CreateAdminRequest request)
	{
		var admin = request.Adapt<Manager>();
		admin = await repository.CreateAsync(admin);
		return admin.Adapt<AdminDto>();
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<IEnumerable<AdminDto>> GetAllAsync(GetAllAdminRequest request)
	{
		var admins = await repository.Items.Paginate(request.PageNumber, request.PageSize).ToListAsync();
		return admins.Adapt<IEnumerable<AdminDto>>();
	}

	public async Task<AdminDto> GetAsync(long id)
	{
		var admin = await repository.GetAsync(id);

		return admin.Adapt<AdminDto>();
	}

	public async Task<AdminDto> UpdateAsync(UpdateAdminRequest request)
	{
		var admin = request.Adapt<Manager>();
		admin = await repository.UpdateAsync(admin);
		return admin.Adapt<AdminDto>();
	}
}
