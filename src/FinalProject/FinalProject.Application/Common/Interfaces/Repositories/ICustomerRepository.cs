using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<int> Add(CustomerDto model);
    Task<int> Update(CustomerDto model);
    Task<int> Remove(int id);
    Task<int> SoftDelete(string customerId);
    Task<CustomerDto> GetById(int id);
    Task<CustomerDto> GetByUserId(string userId);
    Task<IEnumerable<CustomerDto>> GetAll();
}