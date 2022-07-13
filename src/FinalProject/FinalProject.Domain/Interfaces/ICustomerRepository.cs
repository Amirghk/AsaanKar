using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<int> Add(Customer model);
    Task<int> Update(Customer model);
    Task<int> Remove(int id);
    Task<Customer> GetById(int id);
    Task<IEnumerable<Customer>> GetAll();
}