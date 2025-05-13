using Core.Dto;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;


public interface ICuePointService
{
	IEnumerable<CuePointDto> GetAllCuePointsFromRoute(long routeId);
	Task UpdateOrCreateRangeAsync(IEnumerable<CuePointDto> dto);
}