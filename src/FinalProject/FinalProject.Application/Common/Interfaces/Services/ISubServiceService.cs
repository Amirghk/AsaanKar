using FinalProject.Application.Common.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ISubServiceService
{
    Task<int> Set(SubServiceDto dto);
    Task<IEnumerable<SubServiceDto>> GetAll();
    Task<SubServiceDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(SubServiceDto dto);
}
