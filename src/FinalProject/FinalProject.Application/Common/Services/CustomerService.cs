using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
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
        List<CustomerDto> mappedModels = _mapper.Map<List<CustomerDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<CustomerDto> GetById(int id)
    {
        CustomerDto mappedModel = _mapper.Map<CustomerDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CustomerDto dto)
    {
        return await _repository.Add(_mapper.Map<Customer>(dto));
    }

    public async Task<int> Update(CustomerDto dto)
    {
        return await _repository.Update(_mapper.Map<Customer>(dto));
    }
}