using Core.Dto;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface ICuePointService
{
    public Task<CuePoint> CreateCuePoint(CuePoint cuePoint);
    public Task<CuePoint> UpdateCuePoint(CuePoint cuePoint);
    public Task DeleteCuePoint(CuePoint cuePoint);
    public Task<CuePoint> GetCuePoint(long id);
    public CuePointsDto GetAllCuePointsFromRoute(long routeId);
}
