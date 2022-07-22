using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

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
        return await _repository.GetAll();
    }

    public async Task<OrderDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(OrderDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(OrderDto dto)
    {
        return await _repository.Update(dto);
    }
}