using FinalProject.Application.Common.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IExpertService
{
    Task<int> Set(ExpertDto dto);
    Task<IEnumerable<ExpertDto>> GetAll();
    Task<ExpertDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(ExpertDto dto);
    // Task<List<CompoundCustomerDto>> GetAllCompound();
    // Task<CompoundCustomerDto> GetCompoundById(int id);
}
