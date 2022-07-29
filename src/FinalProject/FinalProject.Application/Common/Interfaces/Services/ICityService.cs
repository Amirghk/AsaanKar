using FinalProject.Application.Common.DataTransferObjects;
namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICityService
{
    Task<int> Set(CityDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<CityDto>> GetAll(CancellationToken cancellationToken);
    Task<CityDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(CityDto dto, CancellationToken cancellationToken);
}
