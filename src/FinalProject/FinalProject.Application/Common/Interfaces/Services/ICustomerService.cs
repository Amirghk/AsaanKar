using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<string> Set(CustomerDto dto);
    Task<IEnumerable<CustomerDto>> GetAll();
    Task<CustomerDto> GetById(string id);
    Task<string> SoftDelete(string id);
    Task<string> Remove(string id);
    Task<string> Update(CustomerDto dto);
}
