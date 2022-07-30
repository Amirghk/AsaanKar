using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    private readonly IServiceService _serviceService;
    private readonly IExpertService _expertService;

    public OrderService(
        IOrderRepository repository,
        IMapper mapper,
        ILogger<OrderService> logger,
        IServiceService serviceService,
        IExpertService expertService)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _serviceService = serviceService;
        _expertService = expertService;
    }

    public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}()", "GetAll");
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> GetByUserId(string id, CancellationToken cancellationToken, OrderState? orderState = null)
    {
        return await _repository.GetAll(userId: id, cancellationToken: cancellationToken, orderState: orderState);
    }

    public async Task<OrderDto> GetById(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}({id})", "GetById", id);
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}({id})", "Remove", id);
        return await _repository.Remove(id);
    }

    public async Task<int> Set(OrderDto dto, CancellationToken cancellationToken)
    {
        var service = await _serviceService.GetById(dto.ServiceId, cancellationToken);
        dto.ServiceBasePrice = service.BasePrice;
        _logger.LogTrace("calling and awaiting repository {}({dto})", "Set", dto);
        return await _repository.Add(dto);
    }

    public async Task<int> Update(OrderDto dto, CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}({dto})", "Update", dto);
        return await _repository.Update(dto);
    }
    /// <summary>
    /// returns orders that are available to the expert
    /// (by being in the same city as the expert and having the same skills and being in OrderState 1 or 2)
    /// </summary>
    /// <param name="expertId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// // TODO fix it
    public async Task<IEnumerable<OrderDto>> GetAvailable(string expertId, CancellationToken cancellationToken)
    {
        var expert = await _expertService.GetById(expertId, cancellationToken);
        var services = await _serviceService.GetAll(expertId: expertId, cancellationToken: cancellationToken);
        var stateOneOrders = await _repository.GetAll(cancellationToken, expert.Address!.CityId, orderState: OrderState.WaitingForExpertBid);
        var stateTwoOrders = await _repository.GetAll(cancellationToken, expert.Address!.CityId, orderState: OrderState.WaitingToChooseExpert);
        var orders = stateOneOrders.Concat(stateTwoOrders);
        return orders.Where(x => services.Select(x => x.Id).Contains(x.ServiceId));
    }
}