using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IServiceService
{
    Task<int> Set(ServiceDto dto);
    Task<IEnumerable<ServiceDto>> GetAll();
    Task<ServiceDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(ServiceDto dto);
}
