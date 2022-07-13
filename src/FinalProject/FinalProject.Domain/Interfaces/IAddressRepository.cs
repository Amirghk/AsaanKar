using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IAddressRepository
{
    Task<int> Add(Address model);
    Task<int> Update(Address model);
    Task<int> Remove(int id);
    Task<Address> GetById(int id);
    Task<IEnumerable<Address>> GetAll();
}