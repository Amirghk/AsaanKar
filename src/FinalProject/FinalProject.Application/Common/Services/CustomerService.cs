using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerDto>> GetAll()
    {
        return (await _repository.GetAll()).Where(x => x.IsDeleted == false);
    }

    public async Task<CustomerDto> GetById(string id)
    {
        return await _repository.GetById(id);
    }


    public async Task<string> Remove(string id)
    {
        return await _repository.Remove(id);
    }

    public async Task<string> Set(CustomerDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<string> SoftDelete(string id)
    {
        return await _repository.SoftDelete(id);
    }

    public async Task<string> Update(CustomerDto dto)
    {
        return await _repository.Update(dto);
    }
}