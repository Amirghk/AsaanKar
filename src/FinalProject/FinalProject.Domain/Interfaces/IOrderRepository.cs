using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IOrderRepository
{
    Task<int> Add(Order model);
    Task<int> Update(Order model);
    Task<int> Remove(int id);
    Task<Order> GetById(int id);
    Task<IEnumerable<Order>> GetAll();
}