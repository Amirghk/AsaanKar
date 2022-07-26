using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IAddressService
{
    Task<int> Set(AddressDto dto);
    Task<IEnumerable<AddressDto>> GetAll();
    Task<AddressDto> GetById(int id);
    Task<IEnumerable<AddressDto>> GetByUserId(string userId);
    Task<int> Remove(int id);
    Task<int> Update(AddressDto dto);
    Task<string> GetFullAddressToString(AddressDto dto);
}
