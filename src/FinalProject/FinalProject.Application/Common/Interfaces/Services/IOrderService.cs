using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IOrderService
{
    Task<int> Set(OrderDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<OrderDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(OrderDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<OrderDto>> GetByUserId(string id, CancellationToken cancellationToken, OrderState? fromState = null, OrderState? toState = null);
    Task<IEnumerable<ExpertOrderDto>> GetAvailable(string expertId, CancellationToken cancellationToken);
    Task<int> ExpertBidAdded(int id, CancellationToken cancellationToken);
    Task<int> ApproveBid(int orderId, int bidId, CancellationToken cancellationToken);
}
