using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;

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
    /// <summary>
    /// Checks wether or not the address belongs to the user 
    /// </summary>
    /// <param name="addressId">Id of Address</param>
    /// <param name="userId">Id of User</param>
    /// <param name="addressCategory">Wether the address is for an expert or a customer</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if address is For the selected user, false if not</returns>
    Task<bool> CheckIfAddressIsForUser(int addressId, string userId, AddressCategory addressCategory, CancellationToken cancellationToken);
}
