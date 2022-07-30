using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IAddressService
{
    Task<int> Set(AddressDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<AddressDto>> GetAll(CancellationToken cancellationToken);
    Task<AddressDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<AddressDto>> GetByUserId(string userId, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(AddressDto dto, CancellationToken cancellationToken);
    Task<string> GetFullAddressToString(int id, CancellationToken cancellationToken);
}
