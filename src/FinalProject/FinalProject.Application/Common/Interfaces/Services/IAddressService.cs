using FinalProject.Domain.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IAddressService
{
    Task<int> Set(AddressDto dto);
    Task<IEnumerable<AddressDto>> GetAll();
    Task<AddressDto> GetById(int id);
    Task<IEnumerable<AddressDto>> GetByUserId(int userId);
    Task<int> Remove(int id);
    Task<int> Update(AddressDto dto);
}
