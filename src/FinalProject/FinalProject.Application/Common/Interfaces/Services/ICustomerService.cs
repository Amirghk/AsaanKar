using FinalProject.Application.Common.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<int> Set(CustomerDto dto);
    Task<IEnumerable<CustomerDto>> GetAll();
    Task<CustomerDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(CustomerDto dto);
    Task<List<CustomerQueryDto>> GetAllCompound();
    Task<CustomerQueryDto> GetCompoundById(int id);
}
