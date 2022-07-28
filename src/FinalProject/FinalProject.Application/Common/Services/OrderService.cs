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

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        _logger.LogTrace("calling and awaiting repository {}()", "GetAll");
        return await _repository.GetAll();
    }

    public async Task<IEnumerable<OrderDto>> GetByUserId(string id, OrderState? orderState = null)
    {
        return await _repository.GetByUserId(id, orderState);
    }

    public async Task<OrderDto> GetById(int id)
    {
        _logger.LogTrace("calling and awaiting repository {}({id})", "GetById", id);
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        _logger.LogTrace("calling and awaiting repository {}({id})", "Remove", id);
        return await _repository.Remove(id);
    }

    public async Task<int> Set(OrderDto dto)
    {
        var service = await _serviceService.GetById(dto.ServiceId);
        dto.ServiceBasePrice = service.BasePrice;
        _logger.LogTrace("calling and awaiting repository {}({dto})", "Set", dto);
        return await _repository.Add(dto);
    }

    public async Task<int> Update(OrderDto dto)
    {
        _logger.LogTrace("calling and awaiting repository {}({dto})", "Update", dto);
        return await _repository.Update(dto);
    }
}