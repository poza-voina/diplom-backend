using Core.Dto;
using Core.Entities;

namespace Core.Interfaces.Services;


public interface ICuePointService : ICrudService<CuePointDto, CuePointDto>, ICrudService<CuePoint, CuePointDto>, ICrudServiceById<CuePointDto>
{
	CuePointsDto GetAllCuePointsFromRoute(long routeId);

	Task UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto);
}