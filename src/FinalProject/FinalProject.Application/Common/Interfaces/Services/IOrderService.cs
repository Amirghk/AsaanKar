using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IOrderService
{
    Task<int> Set(OrderDto dto);
    Task<IEnumerable<OrderDto>> GetAll();
    Task<OrderDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(OrderDto dto);
    Task<IEnumerable<OrderDto>> GetByUserId(string id, OrderState? orderState = null);
}
