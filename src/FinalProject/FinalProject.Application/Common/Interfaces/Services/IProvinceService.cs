using FinalProject.Application.Common.DataTransferObjects;
namespace FinalProject.Application.Common.Interfaces.Services;

public interface IProvinceService
{
    Task<int> Set(ProvinceDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<ProvinceDto>> GetAll(CancellationToken cancellationToken);
    Task<ProvinceDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(ProvinceDto dto, CancellationToken cancellationToken);
}
