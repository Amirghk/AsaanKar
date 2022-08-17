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
    /// <summary>
    /// Get a list of all orders according to optional parameters
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="cityId">Optional: city id of order where it was requested</param>
    /// <param name="userId">Optional: Guid of user (customer or expert) who placed or did the order</param>
    /// <param name="fromState">Optional: if fromState is passed without toState method returns only orders in fromState</param>
    /// <param name="toState">Optional: orders which states are between the ranges of fromState and toState(including)</param>
    /// <returns>returns a list of orders which conform to these parameters</returns>
    Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken, int? cityId = null, string? userId = null, OrderState? fromState = null, OrderState? toState = null);
}
