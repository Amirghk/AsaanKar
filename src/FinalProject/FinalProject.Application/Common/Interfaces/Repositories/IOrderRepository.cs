using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<int> Add(OrderDto model);
    Task<int> Update(OrderDto model);
    Task<int> Remove(int id);
    Task<OrderDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<OrderDto>> GetByUserId(string id, CancellationToken cancellationToken, OrderState? orderState = null);
}
