using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<int> Add(CustomerDto model);
    Task<int> Update(CustomerDto model);
    Task<int> Remove(int id);
    Task<CustomerDto> GetById(int id);
    Task<IEnumerable<CustomerDto>> GetAll();
}