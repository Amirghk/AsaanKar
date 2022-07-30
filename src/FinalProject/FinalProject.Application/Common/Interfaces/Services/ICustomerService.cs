using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<string> Set(CustomerDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<CustomerDto>> GetAll(CancellationToken cancellationToken);
    Task<CustomerDto> GetById(string id, CancellationToken cancellationToken);
    Task<string> SoftDelete(string id, CancellationToken cancellationToken);
    Task<string> Remove(string id, CancellationToken cancellationToken);
    Task<string> Update(CustomerDto dto, CancellationToken cancellationToken);
    Task<string> GetName(string id, CancellationToken cancellationToken);
}
