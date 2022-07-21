using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IOrderRepository
{
    Task<int> Add(OrderDto model);
    Task<int> Update(OrderDto model);
    Task<int> Remove(int id);
    Task<OrderDto> GetById(int id);
    Task<IEnumerable<OrderDto>> GetAll();
}