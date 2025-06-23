using Core.Dto.CuePoint;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;


public interface ICuePointService
{
	IEnumerable<CuePointDto> GetAllCuePointsFromRoute(long routeId);
	Task<IEnumerable<CuePointDto>> UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto, long routeId);
}