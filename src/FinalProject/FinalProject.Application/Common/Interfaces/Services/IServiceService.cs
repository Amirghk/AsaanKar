using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IServiceService
{
    Task<int> Set(ServiceDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<ServiceDto>> GetAll(CancellationToken cancellationToken);
    Task<ServiceDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(ServiceDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<ServiceDto>> GetByCategoryId(int categoryId, CancellationToken cancellationToken);
}
