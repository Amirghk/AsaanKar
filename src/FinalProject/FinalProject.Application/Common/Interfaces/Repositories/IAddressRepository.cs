using FinalProject.Application.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<int> Add(AddressDto model);
    Task<int> Update(AddressDto model);
    Task<int> Remove(int id);
    Task<AddressDto> GetById(int id);
    Task<IEnumerable<AddressDto>> GetByUserId(int userId);
    Task<IEnumerable<AddressDto>> GetAll();
}