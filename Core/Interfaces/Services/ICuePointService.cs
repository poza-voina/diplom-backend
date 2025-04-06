using Core.Dto;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;


public interface ICuePointService : ICrudService<CuePointDto, CuePointDto>, ICrudService<CuePoint, CuePointDto>, ICrudServiceById<CuePointDto>
{
	IEnumerable<CuePointDto> GetAllCuePointsFromRoute(long routeId);

	Task UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto);
}