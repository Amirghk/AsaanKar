using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerDto>> GetAll()
    {
        return (await _repository.GetAll()).Where(x => x.IsDeleted == false);
    }

    public async Task<CustomerDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<CustomerDto> GetByUserId(string userId)
    {
        return await _repository.GetByUserId(userId);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CustomerDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> SoftDelete(string customerId)
    {
        return await _repository.SoftDelete(customerId);
    }

    public async Task<int> Update(CustomerDto dto)
    {
        return await _repository.Update(dto);
    }
}