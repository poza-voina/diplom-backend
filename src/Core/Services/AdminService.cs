using Core.Dto.Admin;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class AdminService(IRepository<Admin> repository) : IAdminService
{
	public async Task<AdminResponse> CreateAsync(CreateAdminRequest request)
	{
		var admin = request.Adapt<Admin>();
		admin = await repository.CreateAsync(admin);
		return admin.Adapt<AdminResponse>();
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<IEnumerable<AdminResponse>> GetAllAsync(GetAllAdminRequest request)
	{
		var admins = await repository.Items.Paginate(request.PageNumber, request.PageSize).ToListAsync();
		return admins.Adapt<IEnumerable<AdminResponse>>();
	}

	public async Task<AdminResponse> GetAsync(long id)
	{
		var admin = await repository.GetAsync(id);

		return admin.Adapt<AdminResponse>();
	}

	public async Task<AdminResponse> UpdateAsync(UpdateAdminRequest request)
	{
		var admin = request.Adapt<Admin>();
		admin = await repository.UpdateAsync(admin);
		return admin.Adapt<AdminResponse>();
	}
}
