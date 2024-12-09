using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class CuePointRepository : Repository<CuePoint>, ICuePointRepository
{
	public CuePointRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}
}
