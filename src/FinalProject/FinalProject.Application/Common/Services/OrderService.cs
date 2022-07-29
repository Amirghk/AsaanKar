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

    public OrderService(
        IOrderRepository repository,
        IMapper mapper,
        ILogger<OrderService> logger,
        IServiceService serviceService)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _serviceService = serviceService;
    }

    public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("calling and awaiting repository {}()", "GetAll");
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> GetByUserId(string id, CancellationToken cancellationToken, OrderState? orderState = null)
    {
        return await _repository.GetByUserId(id, orderState, cancellationToken);
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
}