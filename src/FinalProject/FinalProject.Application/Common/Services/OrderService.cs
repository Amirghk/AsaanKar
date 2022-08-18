using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using FinalProject.Domain.Enums;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces;

namespace FinalProject.Application.Common.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    private readonly IServiceService _serviceService;
    private readonly IExpertService _expertService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(
        IOrderRepository repository,
        IMapper mapper,
        ILogger<OrderService> logger,
        IServiceService serviceService,
        IExpertService expertService,
        IDateTimeProvider dateTimeProvider)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _serviceService = serviceService;
        _expertService = expertService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}()", "GetAll");
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> GetByUserId(string id, CancellationToken cancellationToken, OrderState? fromState = null, OrderState? toState = null)
    {
        return await _repository.GetAll(userId: id, cancellationToken: cancellationToken, fromState: fromState, toState: toState);
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
        dto.OrderDate = _dateTimeProvider.Now;
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
    public async Task<IEnumerable<ExpertOrderDto>> GetAvailable(string expertId, CancellationToken cancellationToken)
    {
        var expert = await _expertService.GetById(expertId, cancellationToken);
        var services = await _serviceService.GetAll(expertId: expertId, cancellationToken: cancellationToken);
        var orders = await _repository.GetAll(cancellationToken, expert.Address!.CityId, fromState: OrderState.WaitingForExpertBid, toState: OrderState.WaitingToChooseExpert);

        // get orders that have the same services as the ones the expert offers
        var availableOrders = _mapper.Map<List<ExpertOrderDto>>(orders).Where(x => services.Select(x => x.Id).Contains(x.ServiceId));
        foreach (var order in availableOrders)
        {
            if (order.Bids.Select(x => x.ExpertId).Contains(expertId))
            {
                order.HasBidAlready = true;
                if ((int)order.State > 2)
                {
                    order.IsDeletable = false;
                }
                else
                {
                    order.IsDeletable = true;
                }
            }
        }
        return availableOrders;
    }

    public async Task<int> ExpertBidAdded(int id, CancellationToken cancellationToken)
    {
        var order = await _repository.GetById(id, cancellationToken);
        order.State = OrderState.WaitingToChooseExpert;
        return await _repository.Update(order);
    }

    public async Task<int> ApproveBid(int orderId, int bidId, CancellationToken cancellationToken)
    {
        var order = await _repository.GetById(orderId, cancellationToken);
        var bid = order.Bids.Where(x => x.Id == bidId).SingleOrDefault();
        if (bid == null)
        {
            throw new NotFoundException(nameof(bid), bidId);
        }
        order.ExpertId = bid.ExpertId;
        order.CompletedPrice = bid.Price;
        order.State = OrderState.WaitingForExpertToArrive;
        return await _repository.Update(order);
    }
}