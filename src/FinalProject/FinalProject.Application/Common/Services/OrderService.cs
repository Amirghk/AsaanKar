using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;
    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        List<OrderDto> mappedModels = _mapper.Map<List<OrderDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<OrderDto> GetById(int id)
    {
        OrderDto mappedModel = _mapper.Map<OrderDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(OrderDto dto)
    {
        return await _repository.Add(_mapper.Map<Order>(dto));
    }

    public async Task<int> Update(OrderDto dto)
    {
        return await _repository.Update(_mapper.Map<Order>(dto));
    }
}